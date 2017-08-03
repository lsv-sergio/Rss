using Autofac;
using RssReader.Core.Interfaces.Entities;
using RssReader.Core.Interfaces.Services;
using ServiceObjects.Classes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace RssReader.Core.Classes.Services
{
    public class FeedRssParser: IFeedRssParser
    {
        public IList<IArticle> ParseArticle(IFeed feed, XDocument doc)
        {
            var feedItems = doc.Root.Descendants()
                .First(i => i.Name.LocalName == "channel")
                .Elements().
                Where(i => i.Name.LocalName == "item")
                .Select<XElement, IArticle>(item => 
                DIService.Container.Resolve<IArticle>(new List<NamedParameter>()
                {
                    new NamedParameter("content", item.Elements().First(i => i.Name.LocalName == "description").Value),
                    new NamedParameter("link", item.Elements().First(i => i.Name.LocalName == "link").Value),
                    new NamedParameter("publishDate", ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value)),
                    new NamedParameter("Title", item.Elements().First(i => i.Name.LocalName == "title").Value),
                    new NamedParameter("Feed", feed)
                            }));
            return feedItems.ToList();
        }

        public void ParseFeed(IFeed feed, XDocument doc)
        {
            feed.Description = "Need get description";
        }

        private DateTime ParseDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
                return result;
            else
                return DateTime.MinValue;
        }
    }
}
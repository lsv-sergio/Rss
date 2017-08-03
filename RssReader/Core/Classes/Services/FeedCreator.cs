using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using RssReader.Core.Interfaces.Entities;
using RssReader.Core.Interfaces.Services;
using ServiceObjects.Classes.Services;
using System;
using System.Collections.Generic;

namespace RssReader.Core.Classes.Services
{
    public class FeedCreator : IFeedCreator
    {
        IFeedTransport _feedTransport;

        public FeedCreator(IFeedTransport feedTransport)
        {
            _feedTransport = feedTransport;
        }
        public IFeed CreateAtomFeed(string url, string name)
        {
            var feedAtomParser = DIService.Container.Resolve<IFeedAtomParser>();
            return CreateFeed(url, name, feedAtomParser, FeedTypes.RSS);
        }

        public IFeed CreateRssFeed(string url, string name)
        {
            var feedRssParser = DIService.Container.Resolve<IFeedRssParser>();
            return CreateFeed(url, name, feedRssParser, FeedTypes.RSS);
        }

        private IFeed CreateFeed(string url, string name, IFeedParser feedParser, FeedTypes rSS)
        {
            IList<Parameter> parameters = new List<Parameter>()
            {
                new NamedParameter("name", name),
                new NamedParameter("chanel", this),
                new NamedParameter("url", url),
                new NamedParameter("feedTransport", _feedTransport),
                new NamedParameter("feedParser", feedParser),
                new NamedParameter("feedType", FeedTypes.RSS)
            };
            return DIService.Container.Resolve<IFeed>(parameters);
        }
    }
}

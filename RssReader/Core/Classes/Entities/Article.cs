using RssReader.Core.Interfaces.Entities;
using System;

namespace RssReader.Core.Classes.Entities
{
    public class Article: IArticle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime PublishDate { get; set; }
        public string Link { get; set; }
        public int FeedId { get; set; }
        public IFeed Feed { get; set; }

        public Article(string content, string link, DateTime publishDate, string name, IFeed feed)
        {
            Content = content;
            Link = link;
            PublishDate = publishDate;
            Name = name;
            Feed = feed;
            FeedId = feed.Id;
        }
    }
}

using System;

namespace RssReader.Core.Interfaces.Entities
{
    public interface IArticle: IEntity
    {
        string Content { get; set; }
        string Summary { get; set; }
        DateTime PublishDate { get; set; }
        string Link { get; set; }
        int FeedId { get; set; }
        IFeed Feed { get; set; }
    }
}
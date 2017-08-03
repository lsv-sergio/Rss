using RssReader.Core.Interfaces.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Entities
{
    public interface IFeed:IEntity, ISaved, IDeleted
    {
        string Description { get; set; }
        int ChanelId { get; set; }
        IChannel Chanel { get; set; }
        string Url { get; set; }
        FeedTypes FeedType { get; set; }
        IList<IArticle> Articles { get; set; }
        Task<IList<IArticle>> GetAllArticles();
        Task CompleteFeed();
    }
}
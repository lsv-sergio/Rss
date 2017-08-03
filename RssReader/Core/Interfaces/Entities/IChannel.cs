using RssReader.Core.Interfaces.Commands;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Entities
{
    public interface IChannel:IEntity, ISaved, IDeleted
    {
        string Description { get; set; }
        IList<IFeed> Feeds { get; set; }
        Task<IList<IArticle>> GetAllarticle();
        Task<IFeed> AddRssFeed(string url, string name);
        Task<IFeed> AddAtomFeed(string url, string name);
    }
}

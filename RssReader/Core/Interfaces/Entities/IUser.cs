using RssReader.Core.Interfaces.Commands;
using System.Collections.Generic;

namespace RssReader.Core.Interfaces.Entities
{
    public interface IUser: IEntity, ISaved, IDeleted
    {
        IList<IChannel> Channels { get; set; }
    }
}

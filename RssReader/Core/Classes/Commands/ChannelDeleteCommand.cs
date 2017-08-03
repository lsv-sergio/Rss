using Autofac;
using Microsoft.AspNetCore.Http;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class ChannelDeleteCommand : IChannelDeleteCommand
    {
        public async Task Delete(UnitOfWork uow, IChannel entity)
        {
            var repository = uow.GetRepository<IChannel>();
            foreach(var feed in entity.Feeds)
            {
                await feed.Delete(uow);
            }
            await repository.Delete(entity);
        }

        public async Task Delete(IChannel entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Delete(ouw, entity);
            await ouw.Commit();
        }
    }
}

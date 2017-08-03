using Autofac;
using Microsoft.EntityFrameworkCore;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class ChannelSaveCommand : IChannelSaveCommand
    {
        public async Task Save(IChannel entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Save(ouw, entity);
            await ouw.Commit();
        }

        public async Task Save(UnitOfWork uow, IChannel entity)
        {
            var repository = uow.GetRepository<IChannel>();
            var existingChanel = await repository.GetForSave(entity.Id)
                .Include(x => x.Feeds).FirstOrDefaultAsync();

            if (existingChanel != null)
            {
                await repository.Update(entity);
                foreach (var existingFeed in existingChanel.Feeds)
                {
                    if (!entity.Feeds.Any(c => c.Id == existingFeed.Id))
                    {
                        await existingFeed.Delete(uow);
                    }
                }

                foreach (var childFeed in entity.Feeds)
                {
                    var existingChild = existingChanel.Feeds
                        .Where(c => c.Id == childFeed.Id)
                        .SingleOrDefault();

                    await childFeed.Save(uow);
                }
            }
            else
            {
                await repository.Create(entity);
                foreach (var feed in entity.Feeds)
                {
                    await feed.Save(uow);
                }
            }
        }
    }
}

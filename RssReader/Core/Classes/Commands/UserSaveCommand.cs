using Autofac;
using Microsoft.EntityFrameworkCore;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class UserSaveCommand : IUserSaveCommand
    {
        public async Task Save(IUser entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Save(ouw, entity);
            await ouw.Commit();
        }

        public async Task Save(UnitOfWork uow, IUser entity)
        {
            var repository = uow.GetRepository<IUser>();
            var existingUser = await repository.GetForSave(entity.Id)
                .Include(x => x.Channels).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                await repository.Update(entity);
                foreach (var existingChannel in existingUser.Channels)
                {
                    if (!entity.Channels.Any(c => c.Id == existingChannel.Id))
                    {
                        await existingChannel.Delete(uow);
                    }
                }

                foreach (var childChannel in entity.Channels)
                {
                    var existingChannel = existingUser.Channels
                        .Where(c => c.Id == childChannel.Id)
                        .SingleOrDefault();

                    await childChannel.Save(uow);
                }
            }
            else
            {
                await repository.Create(entity);
                foreach (var channel in entity.Channels)
                {
                    await channel.Save(uow);
                }
            }
        }
    }
}

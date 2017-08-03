using Autofac;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class UserDeleteCommand : IUserDeleteCommand
    {
        public async Task Delete(UnitOfWork uow, IUser entity)
        {
            var repository = uow.GetRepository<IUser>();
            foreach(var chanel in entity.Channels)
            {
                await chanel.Delete(uow);
            }
            await repository.Delete(entity);
        }

        public async Task Delete(IUser entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Delete(ouw, entity);
            await ouw.Commit();
        }
    }
}

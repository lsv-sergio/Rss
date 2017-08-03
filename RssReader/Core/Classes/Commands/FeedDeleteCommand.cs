using Autofac;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class FeedDeleteCommand: IFeedDeleteCommand
    {
        public async Task Delete(UnitOfWork uow, IFeed entity)
        {
            var repository = uow.GetRepository<IFeed>();
            await repository.Delete(entity);
        }

        public async Task Delete(IFeed entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Delete(ouw, entity);
            await ouw.Commit();
        }
    }
}

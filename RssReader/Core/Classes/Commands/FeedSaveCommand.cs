using Autofac;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Commands
{
    public class FeedSaveCommand: IFeedSaveCommand
    {
        public async Task Save(UnitOfWork uow, IFeed entity)
        {
            var repository = uow.GetRepository<IFeed>();
            await repository.Update(entity);
        }

        public async Task Save(IFeed entity)
        {
            var ouw = DIService.Container.Resolve<UnitOfWork>();
            await Save(ouw, entity);
            await ouw.Commit();
        }
    }
}

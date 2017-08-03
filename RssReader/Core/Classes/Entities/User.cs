using Autofac;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Entities
{
    public class User: IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<IChannel> Channels { get; set; }

        private IUserDeleteCommand _deleteCommand;
        private IUserSaveCommand _saveCommand;

        public void Init()
        {
            _saveCommand = DIService.Container.Resolve<IUserSaveCommand>();
            _deleteCommand = DIService.Container.Resolve<IUserDeleteCommand>();
        }

        public User()
        {
            Init();
        }

        public async Task Delete()
        {
            await _deleteCommand.Delete(this);
        }

        public async Task Delete(UnitOfWork uow)
        {
            await _deleteCommand.Delete(uow, this);
        }

        public async Task Save()
        {
            await _saveCommand.Save(this);
        }

        public async Task Save(UnitOfWork uow)
        {
            await _saveCommand.Save(uow, this);
        }
    }
}

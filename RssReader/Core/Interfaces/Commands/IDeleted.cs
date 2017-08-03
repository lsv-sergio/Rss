using RssReader.Core.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Commands
{
    public interface IDeleted
    {
        Task Delete();
        Task Delete(UnitOfWork uow);
    }
}

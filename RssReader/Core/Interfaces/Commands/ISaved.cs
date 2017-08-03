using RssReader.Core.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Commands
{
    public interface ISaved
    {
        Task Save();
        Task Save(UnitOfWork uow);
    }
}

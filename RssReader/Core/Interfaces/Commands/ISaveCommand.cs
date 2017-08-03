using RssReader.Core.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Commands
{
    public interface ISaveCommand<T>
    {
        Task Save(UnitOfWork ouw, T entity);
        Task Save(T entity);
    }
}

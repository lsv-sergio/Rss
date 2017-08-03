using RssReader.Core.Classes.Services;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Commands
{
    public interface IDeleteCommand<T>
    {
        Task Delete(UnitOfWork ouw, T entity);
        Task Delete(T entity);
    }
}

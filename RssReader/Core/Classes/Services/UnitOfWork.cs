using Autofac;
using RssReader.Core.Classes.Repositories;
using RssReader.Core.Interfaces.Entities;
using ServiceObjects.Classes.Services;
using System;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Services
{
    public class UnitOfWork: IDisposable
    {
        private ReaderDbContext _dbContext;
        private bool disposed = false;

        public UnitOfWork(ReaderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Repository<T> GetRepository<T>() where T: class, IEntity
        {
            return DIService.Container.Resolve<Repository<T>>(new NamedParameter("dbSet", _dbContext.Set<T>()));
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

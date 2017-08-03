using Microsoft.EntityFrameworkCore;
using RssReader.Core.Interfaces;
using RssReader.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Repositories
{
    public class Repository<T> where T: class, IEntity
    {
        private DbSet<T> _dbSet;
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetForSave(int id)
        {
            return _dbSet.Where(x => x.Id == id).AsQueryable();
        }

        public async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await new Task(() =>
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            });
        }
        public async Task Delete(T entity)
        {
           await new Task(() =>
           {
               if (entity != null)
               {
                   _dbSet.Remove(entity);
               }
           });
        }

    }
}

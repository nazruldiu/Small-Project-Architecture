using Architecture.DataService.Data;
using Architecture.DataService.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _dbContext;
        protected DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
          return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
           return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var item = await _dbSet.FindAsync(id);
            _dbSet.Remove(item);
            return true;
        }

        public virtual bool Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }
    }
}

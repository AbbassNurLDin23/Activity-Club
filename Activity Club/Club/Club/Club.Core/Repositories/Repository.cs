using Club.Core.DataModels;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MyDataContext _DataContext;
        public Repository(MyDataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _DataContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _DataContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _DataContext.Set<TEntity>().Update(entity);
            await _DataContext.SaveChangesAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _DataContext.Set<TEntity>().AddAsync(entity);
            await _DataContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _DataContext.Set<TEntity>().AddRangeAsync(entities);
            await _DataContext.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _DataContext.Set<TEntity>().Remove(entity);
            await _DataContext.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            _DataContext.Set<TEntity>().RemoveRange(entities);
            await _DataContext.SaveChangesAsync();
        }
    }
}

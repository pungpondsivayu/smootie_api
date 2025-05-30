using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SmootieerpDbConText _db;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(SmootieerpDbConText db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<List<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAll() => _dbSet.AsQueryable();
        public async Task<TEntity?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}

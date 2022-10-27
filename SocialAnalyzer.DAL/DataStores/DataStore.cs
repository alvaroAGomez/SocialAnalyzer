using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.DataStores
{
    public abstract class DataStore<TEntity> : IDataStore<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public DataStore(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _dbSet.CountAsync(condition) > 0;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _dbSet.Where(condition).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _dbSet.SingleOrDefaultAsync(condition);
        }

        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        //}

        public async Task<TEntity> InsertAndSaveAsync(TEntity entity)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // if (await ExistsAsync(x => x.Id == entity.Id)) return null; // ponerlo en el servicio direcrtamente 

                    _dbContext.Add(entity);
                    _dbContext.SaveChanges();

                    _dbContext.Entry(entity).State = EntityState.Detached;
                    dbContextTransaction.Commit();
                    return entity;
                }
                catch (Exception e)
                {
                    dbContextTransaction?.Rollback();
                    return null;
                }
            }

        }


        public async Task<TEntity> UpdateAndSaveAsync(Expression<Func<TEntity, bool>> condition, TEntity entity, string[] excludedProperties)
        {
            if (!await ExistsAsync(condition)) return null;
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Attach(entity);

                    _dbContext.Entry(entity).State = EntityState.Modified;

                    if (excludedProperties != null)
                        foreach (var property in excludedProperties)
                        {
                            _dbContext.Entry(entity).Property(property).IsModified = false;
                        }

                    _dbContext.SaveChanges();

                    _dbContext.Entry(entity).State = EntityState.Detached;
                    dbContextTransaction.Commit();
                    return entity;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return null;
                    throw e;
                }
            }
        }
    }
}

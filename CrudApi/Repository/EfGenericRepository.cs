using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoLog;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Repository
{
    public class EfGenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    // IDisposable использую по документации Microsoft. Лично неуверен в необходимости этого, т.к.
    // насколько я знаю- платформа сама очищает неуправляемые ресурсы
    {
        private DataContext Context { get; }
        private DbSet<TEntity> DbSet { get; }
        private ILogger Logger { get; }

        public EfGenericRepository(DataContext context, ILogger logger)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            Logger = logger;
        }
        
        public async Task<bool> Create(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return false;
            }
        }

        public async Task<TEntity> Get(int id)
        {
            try
            {
                return await DbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return null;
            }
        }
        
        public async Task<IEnumerable<TEntity>> Get()
        {
            try
            {
                return DbSet.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return null;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                TEntity entity = await Get(id);
                await Delete(entity);
                return true;
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return false;
            }
        }
        
        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await Logger.Warning(e.Message);
                return false;
            }
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
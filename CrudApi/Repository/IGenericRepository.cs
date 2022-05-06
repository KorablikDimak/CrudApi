using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApi.Repository
{
    /// <summary>
    /// используются только асинхронные методы.
    /// при необходимости можно их вызывать синхронно или написать синхронные методы.
    /// </summary>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
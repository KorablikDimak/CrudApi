using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApi.Repository
{
    /// <summary>
    /// используются только асинхронные методы.
    /// при необходимости можно их вызывать синхронно или написать синхронные методы.
    /// методы с возвращаемым bool сделал чтобы можно было из контроллера отследить выполнение.
    /// писать Try/обычную версию метода не стал т.к. в контроллере используются лишь такие определения методов
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
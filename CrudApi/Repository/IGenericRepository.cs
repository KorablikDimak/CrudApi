using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApi.Repository
{
    /// <summary>
    /// используются все методы асинхронными так как работать с бд синхронно нет смысла,
    /// при желании в любом случае можно их вызывать синхронно.
    /// методы с возвращаемым bool сделал чтобы можно было из контроллера отследить выполнение.
    /// писать Try/обычную версию метода не стал т.к. в контроллере все равно используются лишь такие определения методов
    /// </summary>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> Create(TEntity entity);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> Get();
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(TEntity entity);
    }
}
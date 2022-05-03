using System.Collections.Generic;
using System.Threading.Tasks;
using CrudApi.Repository;
using InfoLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    // [Authorize]
    [ApiController]
    public class EntityController<TEntity> : Controller where TEntity : class
    {
        private IGenericRepository<TEntity> GenericRepository { get; }

        public EntityController(DataContext context, ILogger logger)
        {
            GenericRepository = new EfGenericRepository<TEntity>(context, logger);
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult> CreateEntity(TEntity entity)
        {
            if (await GenericRepository.CreateAsync(entity)) return Ok();
            return new ConflictResult();
        }

        [HttpGet("GetId")]
        public async Task<ActionResult<TEntity>> GetEntity(int id)
        {
            var entity = await GenericRepository.GetAsync(id);
            if (entity == null) return new NotFoundResult();
            return entity;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetEntities()
        {
            var entities = await GenericRepository.GetAsync();
            if (entities == null) return new NotFoundResult();
            return new ActionResult<IEnumerable<TEntity>>(entities);
        }

        [HttpPatch("Update")]
        public async Task<ActionResult> UpdateEntity(TEntity entity)
        {
            if (await GenericRepository.UpdateAsync(entity)) return Ok();
            return new ConflictResult();
        }
        
        [HttpDelete("DeleteId")]
        public async Task<ActionResult> DeleteEntity(int id)
        {
            if (await GenericRepository.DeleteAsync(id)) return Ok();
            return new ConflictResult();
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteEntity(TEntity entity)
        {
            if (await GenericRepository.DeleteAsync(entity)) return Ok();
            return new ConflictResult();
        }
    }
}
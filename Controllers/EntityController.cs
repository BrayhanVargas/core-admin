using Microsoft.AspNetCore.Mvc;
using core_admin.Repositories;
using core_admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace core_admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;

        public EntityController(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEntity([FromBody] Entity entity)
        {
            var result = await _entityRepository.CreateEntity(entity);
            return Ok(result);
        }


        // [HttpPut("edit/{id}")]
        // public IActionResult EditEntity(int id, [FromBody] Entity entity)
        // {
        //     return Ok(_entityRepository.EditEntity(id, entity));
        // }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            var result = await _entityRepository.DeleteEntity(id);

            if (result.Contains("was not found"))
            {
                return NotFound(result);
            }

            return Ok(result);
        }


        // [HttpGet("{id}")]
        // public IActionResult GetEntity(int id)
        // {
        //     return Ok(_entityRepository.GetEntity(id));
        // }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEntities()
        {
            try
            {
                var entities = await _entityRepository.GetAllEntities();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                // Retorna el error con detalles para poder rastrear mejor
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

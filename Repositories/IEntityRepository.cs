using core_admin.Models;

namespace core_admin.Repositories
{
    public interface IEntityRepository
    {
        Task<Entity> CreateEntity(Entity entity);
        // Task<Entity> EditEntity(int id, Entity entity);
        Task<string> DeleteEntity(int id);
        // Task<Entity?> GetEntity(int id);
        Task<IEnumerable<DTOGetlAllEntitiesResponse>> GetAllEntities();
    }
}

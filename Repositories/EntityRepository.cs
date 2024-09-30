using core_admin.Data;
using core_admin.Models;
using Microsoft.EntityFrameworkCore;

namespace core_admin.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Entity> CreateEntity(Entity entity)
        {
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<string> DeleteEntity(int id)
        {
            var hasEmployees = await _context.Employees.AnyAsync(e => e.EntityId == id);

            var query = _context.Entities.AsQueryable();

            if (hasEmployees)
            {
                query = query.AsNoTracking().Include(e => e.Employees);
            }

            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return $"Entity with ID {id} was not found.";
            }

            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();

            return $"Entity with ID {id} and associated employees were successfully deleted.";
        }


        public async Task<IEnumerable<DTOGetlAllEntitiesResponse>> GetAllEntities()
        {
            try
            {
                var entities = await _context.Entities
                    .Include(e => e.Employees)
                    .Select(e => new DTOGetlAllEntitiesResponse
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        Email = e.Email,
                        Phone = e.Phone,
                        Employees = e.Employees,
                        EmployeeCount = e.Employees != null ? e.Employees.Count : 0
                    })
                    .ToListAsync();

                if (!entities.Any())
                {
                    Console.WriteLine("No entities found.");
                    return new List<DTOGetlAllEntitiesResponse>();
                }

                Console.WriteLine($"Found {entities.Count} entities.");
                return entities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entities: {ex.Message}");
                throw new Exception($"Error fetching entities: {ex.Message}");
            }
        }

    }
}

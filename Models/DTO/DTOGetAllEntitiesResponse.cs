namespace core_admin.Models
{
    public class DTOGetlAllEntitiesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        public ICollection<Employee>? Employees { get; set; }

        public int EmployeeCount { get; set; }
    }
}
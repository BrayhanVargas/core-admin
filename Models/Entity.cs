namespace core_admin.Models
{
    public class Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public ICollection<Employee>? Employees { get; set; }
}

}

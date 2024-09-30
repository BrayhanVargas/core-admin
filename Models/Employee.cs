namespace core_admin.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Position { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public int EntityId { get; set; }
    }
}

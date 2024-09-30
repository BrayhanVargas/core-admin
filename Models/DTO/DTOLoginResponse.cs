namespace core_admin.Models.DTO
{
    public class DTOLoginResponse
    {
        public required string Token { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Id { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}

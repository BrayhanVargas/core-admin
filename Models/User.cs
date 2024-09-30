using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace core_admin.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "varchar(30)")]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(30)")]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public required string LastName { get; set; }

        public string? Token { get; set; }

    }
}

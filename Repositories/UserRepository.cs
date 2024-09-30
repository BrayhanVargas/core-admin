using core_admin.Data;
using core_admin.Models;
using core_admin.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace core_admin.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        // Implementación del método GetTestMessage
        public string GetTestMessage()
        {
            return "Hello from UserRepository";
        }

        // Register User Method
        public async Task<string> RegisterUserAsync(DTORegister model)
        {
            // Check if the email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return "Email already exists";
            }

            var user = new User
            {
                UserName = model.FirstName,
                Email = model.Email,
                Name = model.FirstName,
                LastName = model.LastName,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("User"))
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                return "User registered successfully";
            }

            return string.Join(", ", result.Errors.Select(e => e.Description));
        }


        public async Task<DTOLoginResponse> LoginUserAsync(DTOLogin model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email) ?? throw new Exception("Invalid credentials");
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                // Verificar si las configuraciones necesarias no son nulas
                var jwtKey = _configuration["Jwt:Key"];
                var jwtIssuer = _configuration["Jwt:Issuer"];
                var jwtAudience = _configuration["Jwt:Audience"];

                // Validar que las configuraciones no sean nulas o vacías
                if (string.IsNullOrEmpty(jwtKey))
                {
                    throw new Exception("JWT Key is missing in the configuration.");
                }

                if (string.IsNullOrEmpty(jwtIssuer))
                {
                    throw new Exception("JWT Issuer is missing in the configuration.");
                }

                if (string.IsNullOrEmpty(jwtAudience))
                {
                    throw new Exception("JWT Audience is missing in the configuration.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var mainRole = roles.FirstOrDefault() ?? "User";

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email?? ""),
                new Claim(ClaimTypes.Role, mainRole)
            }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = jwtIssuer,
                    Audience = jwtAudience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new DTOLoginResponse
                {
                    Token = tokenHandler.WriteToken(token),
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Role = mainRole,
                    Id = user.Id,
                    TokenExpiration = tokenDescriptor.Expires ?? DateTime.UtcNow.AddHours(1)
                };
            }

            throw new Exception("Invalid credentials");
        }

    }
}

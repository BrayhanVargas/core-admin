using core_admin.Models.DTO;
using core_admin.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace core_admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet("get")]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetTestMessage());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTORegister model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterUserAsync(model);

                if (result == "User registered successfully")
                {
                    return Ok(new { message = result });
                }

                return BadRequest(new { message = result });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOLogin model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userResponse = await _userRepository.LoginUserAsync(model);

                    return Ok(userResponse);
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

    }
}

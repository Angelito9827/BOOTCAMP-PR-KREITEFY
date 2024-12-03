using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _token;

        public AuthController(IUserService userService, ITokenService token)
        {
            _userService = userService;
            _token = token;
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public ActionResult Register([FromBody] UserDto userDto)
        {

            try
            {
                var existingUser = _userService.GetAllUsers().FirstOrDefault(u => u.Email == userDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("This email is alredy in use.");
                }
                var newUser = _userService.RegisterUser(userDto);
                var token = _token.GenerateJwtToken(newUser);

                return Ok(new
                {
                    User = newUser,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public ActionResult<LoginDto> Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.GetAllUsers()
                .FirstOrDefault(u => u.Email.Equals(loginDto.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null || user.Password != loginDto.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _token.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

    }
}

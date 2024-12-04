using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _token;

        public AuthController(IAuthService authService, ITokenService token)
        {
            _authService = authService;
            _token = token;
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public ActionResult Register([FromBody] UserRegisterDto request)
        {
            try
            {
                _authService.Register(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [Produces("application/json")]
        [AllowAnonymous]
        public ActionResult<LoginDto> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = _authService.Login(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized("Invalid email or password.");
            }
        }

    }
}

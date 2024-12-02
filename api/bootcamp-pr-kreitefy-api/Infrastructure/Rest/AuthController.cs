using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public ActionResult Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingUser = _userService.GetAllUsers().FirstOrDefault(u => u.Email == userDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("This email is alredy in use.");
                }

                // Registra al nuevo usuario
                var newUser = _userService.RegisterUser(userDto);

                // Genera el token para el usuario registrado
                var token = GenerateJwtToken(newUser);

                // Devuelve el usuario, el token y el nombre
                return Ok(new
                {
                    User = newUser,
                    Token = token,
                    UserName = newUser.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJwtToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(RandomNumberGenerator.GetBytes(32));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public AuthDto Login(LoginDto loginDto)
        {
            var user = _userService.GetUserByEmail(loginDto.Email);
            if (user == null || user.Password != loginDto.Password)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _tokenService.GenerateJwtToken(user);

            return new AuthDto
            {
                Token = token
            };
        }

        public AuthDto Register(UserDto userDto)
        {
            var existingUser = _userService.GetUserByEmail(userDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var newUser = _userService.RegisterUser(userDto);
            var token = _tokenService.GenerateJwtToken(newUser);

            return new AuthDto
            {
                Token = token
            };
        }
    }
}

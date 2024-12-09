using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Validators;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserService userService, ITokenService tokenService, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
        }

        public AuthDto Login(LoginDto loginDto)
        {

            if (!EmailValidator.IsValidEmail(loginDto.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            var user = _userService.GetUserByEmail(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!_passwordHasher.VerifyPassword(user.Password, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _tokenService.GenerateJwtToken(user);

            return new AuthDto
            {
                Token = token
            };
        }

        public AuthDto Register(UserRegisterDto request)
        {
            if (!EmailValidator.IsValidEmail(request.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            var existingUser = _userService.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var passwordErrors = PasswordValidator.ValidatePassword(request.Password).ToList();
            if (passwordErrors.Any())
            {
                throw new ArgumentException(string.Join(", ", passwordErrors));
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var role = _roleRepository.GetById(request.RoleId);

            var userDto = new UserDto()
            {
                Email = request.Email,
                LastName = request.LastName,
                Name = request.Name,
                Password = hashedPassword,
                RoleId = request.RoleId,
                RoleName = role.Name,
            };

            var newUser = _userService.RegisterUser(userDto);
            var token = _tokenService.GenerateJwtToken(newUser);
            return new AuthDto
            {
                Token = token
            };
        }
    }
}

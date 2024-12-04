using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IAuthService
    {
        void Register(UserRegisterDto userDto);

        AuthDto Login(LoginDto loginDto);
    }
}

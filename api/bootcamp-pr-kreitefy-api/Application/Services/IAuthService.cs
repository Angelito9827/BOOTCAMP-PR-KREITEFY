using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IAuthService
    {
        AuthDto Register(UserRegisterDto userDto);

        AuthDto Login(LoginDto loginDto);
    }
}

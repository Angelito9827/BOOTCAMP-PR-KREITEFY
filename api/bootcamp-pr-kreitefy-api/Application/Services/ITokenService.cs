using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDto user);
    }
}

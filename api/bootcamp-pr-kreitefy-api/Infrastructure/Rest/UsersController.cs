using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/users")]
    [ApiController]
    public class UsersController : GenericCrudController<UserDto>
    {
        public UsersController(IUserService userService) : base(userService)
        {
        }
    }
}

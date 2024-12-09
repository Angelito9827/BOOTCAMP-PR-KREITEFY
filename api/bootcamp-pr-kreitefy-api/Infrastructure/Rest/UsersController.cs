using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/users")]
    [ApiController]
    [Authorize]
    public class UsersController : GenericCrudController<UserDto>
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
        [NonAction]
        public override ActionResult<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<UserDto> GetAllUsersWithRoleName()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}


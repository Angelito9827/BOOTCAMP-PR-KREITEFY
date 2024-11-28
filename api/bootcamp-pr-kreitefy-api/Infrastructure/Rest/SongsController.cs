using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/songs")]
    [ApiController]
    public class SongsController : GenericCrudController<SongDto>
    {
        private readonly ISongService _songService;
        public SongsController(ISongService songService) : base(songService)
        {
            _songService = songService;
        }

        [NonAction]
        public override ActionResult<IEnumerable<SongDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<SongDto> GetAllUsersWithRoleName()
        {
            return Ok(_songService.GetAllSongs());
        }
    }
}

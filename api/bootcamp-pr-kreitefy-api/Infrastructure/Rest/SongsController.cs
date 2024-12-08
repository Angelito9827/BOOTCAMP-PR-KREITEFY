using bootcamp_framework.Infraestructure.Rest;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/songs")]
    [ApiController]
    [Authorize]
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

        [HttpGet("/songs/recent-songs")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<RecentSongDto>> GetRecentSongs([FromQuery] int count = 5)
        {
            try
            {
                var songs = _songService.GetRecentSongs(count);
                return Ok(songs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

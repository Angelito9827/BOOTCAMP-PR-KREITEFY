using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult AddScore([FromBody] ScoreDto scoreDto)
        {
            try
            {
                var score = _scoreService.AddScore(scoreDto.UserId, scoreDto.SongId, scoreDto.Stars);
                return Ok(score);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

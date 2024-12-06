using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost("increment-playcount")]
        [Produces("application/json")]
        public IActionResult IncrementPlayCount([FromQuery] long userId, [FromQuery] long songId)
        {
            try
            {
                var result = _historyService.IncrementPlayCount(userId, songId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

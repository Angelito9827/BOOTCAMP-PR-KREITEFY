using bootcamp_framework.Application;
using bootcamp_pr_kreitefy_api.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Rest
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("user/{userId}/recommendedsongs")]
        [Produces("application/json")]
        public IActionResult GetRecommendeSongs([FromRoute] long userId)
        {
            try
            {
                var songsForMe = _historyService.GetRecommendedSongsForUser(userId);
                if (songsForMe == null || !songsForMe.Any())
                {
                    return NotFound(new { Message = "No personalized songs found for this user." });
                }

                return Ok(songsForMe);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("user/{userId}/history")]
        [Produces("application/json")]
        public IActionResult GetHistory([FromRoute] long userId, [FromQuery] PaginationParameters paginationParameters)
        {
            try
            {
                var history = _historyService.GetHistorySongs(userId, paginationParameters);
                if (history == null || !history.Any())
                {
                    return NotFound(new { Message = "No history found for this user." });
                }

                var response = new
                {
                    CurrentPage = history.CurrentPage,
                    TotalPages = history.TotalPage,
                    PageSize = history.PageSize,
                    TotalCount = history.TotalCount,
                    Items = history
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

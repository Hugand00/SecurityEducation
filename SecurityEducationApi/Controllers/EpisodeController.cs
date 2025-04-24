using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EpisodeController : ControllerBase
	{
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpGet("Episodes")]
        public async Task<IActionResult> GetEpisodesByChapterId(int chapterId)
        {
            var result = await _episodeService.GetEpisodesByChapterId(chapterId);
            return Ok(result);
        }

    }
}

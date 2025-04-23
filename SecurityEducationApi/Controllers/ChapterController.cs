using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChapterController : ControllerBase
	{
		private readonly IChapterService _chapterService;

		public ChapterController(IChapterService chapterService)
		{
			_chapterService = chapterService;
		}

		[HttpGet("Chapters")]
		public async Task<IActionResult> GetEveryChapter()
		{
			var result = await _chapterService.GetEveryChapter();
			return Ok(result);
		}
	}
}

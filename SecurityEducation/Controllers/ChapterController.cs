using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Services.Interfaces;

namespace SecurityEducation.Controllers
{
	public class ChapterController : Controller
	{
		private readonly IChapterService _chapterService;

		public ChapterController(IChapterService chapterService)
		{
			_chapterService = chapterService;
			
		}
		public async Task<IActionResult> Index()
		{
			var model = await _chapterService.GetEveryChapter();
			return View(model);
		}
	}
}

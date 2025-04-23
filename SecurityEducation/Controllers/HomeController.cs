using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Models;
using SecurityEducation.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SecurityEducation.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IChapterService _chapterService;

		public HomeController(ILogger<HomeController> logger, IChapterService chapterService)
		{
			_logger = logger;
			_chapterService = chapterService;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _chapterService.GetEveryChapter();
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

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
		private readonly IOverviewService _overviewService;

		public HomeController(ILogger<HomeController> logger, IOverviewService overviewService)
		{
			_logger = logger;
			_overviewService = overviewService;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _overviewService.GetChaptersAndEpisodes();
			
			return View(model);
		}

        public IActionResult EducationInfo()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult AccessibilityInfo()
        {
            return View();
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

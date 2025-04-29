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

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			
		}

		public async Task<IActionResult> Index()
		{
			return View();
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

using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Services.Interfaces;
using System.Threading.Tasks;

namespace SecurityEducation.Controllers
{
	public class OverviewController : Controller
	{
		private readonly IOverviewService _overviewService;
		public OverviewController(IOverviewService overviewService)
		{
			_overviewService = overviewService;
		}
		[HttpGet("Overview/Overviews/")]
		public async Task<IActionResult> Overviews()
		{
			var model = await _overviewService.GetChaptersAndEpisodes();

			return View(model);
		}
	}
}

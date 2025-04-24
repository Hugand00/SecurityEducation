using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Services.Interfaces;
using System.Threading.Tasks;

namespace SecurityEducation.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {            
            _episodeService = episodeService;
        }

        [HttpGet("Episode/Episodes/{id}")]
        public async Task<IActionResult> Episodes(int id)
        {
            var model = await _episodeService.GetEpisodesByChapterId(id);

            return View(model);
        }
    }
}

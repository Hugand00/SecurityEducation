using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Services.Interfaces;

namespace SecurityEducation.Controllers
{
    public class ReadingMaterialController : Controller
    {
        private readonly IReadingMaterialService _readingMaterialService;
        private readonly IEpisodeService _episodeService;

        public ReadingMaterialController(IReadingMaterialService readingMaterialService, IEpisodeService episodeService)
        {
            _readingMaterialService = readingMaterialService;
            _episodeService = episodeService;
        }

        [HttpGet("/ReadingMaterial/ReadingMaterials/{chapterId}/{episodeId}")]
        public async Task<IActionResult> ReadingMaterials(int chapterId, int episodeId)
        {
            var model = await _readingMaterialService.GetReadingMaterialByEpisodeId(episodeId);
            var episode = await _episodeService.GetEpisodeById(episodeId, chapterId);

            ViewBag.EpisodeName = episode.Name;
            ViewBag.ChapterId = chapterId;
            return View(model);
        }
    }
}

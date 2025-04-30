using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Services.Interfaces;

namespace SecurityEducation.Controllers
{
    public class ReadingMaterialController : Controller
    {
        private readonly IReadingMaterialService _readingMaterialService;

        public ReadingMaterialController(IReadingMaterialService readingMaterialService)
        {
            _readingMaterialService = readingMaterialService;
        }

        [HttpGet("/ReadingMaterial/ReadingMaterials/{chapterId}/{episodeId}")]
        public async Task<IActionResult> ReadingMaterials(int chapterId, int episodeId)
        {
            var model = await _readingMaterialService.GetReadingMaterialByEpisodeId(episodeId);

            ViewBag.ChapterId = chapterId;
            return View(model);
        }
    }
}

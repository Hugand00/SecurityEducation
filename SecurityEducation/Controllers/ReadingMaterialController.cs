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

        public async Task<IActionResult> ReadingMaterials()
        {
            var model = await _readingMaterialService.GetReadingMaterialByEpisodeId();
            return View(model);
        }
    }
}

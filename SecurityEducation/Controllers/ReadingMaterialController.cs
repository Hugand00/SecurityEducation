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

        [HttpGet("/ReadingMaterial/ReadingMaterials/{id}")]
        public async Task<IActionResult> ReadingMaterials(int id)
        {
            var model = await _readingMaterialService.GetReadingMaterialByEpisodeId(id);
            return View(model);
        }
    }
}

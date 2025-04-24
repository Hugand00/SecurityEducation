using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingMaterialController : ControllerBase
    {
        private readonly IReadingMaterialService _readingMaterialService;

        public ReadingMaterialController(IReadingMaterialService readingMaterialService)
        {
            _readingMaterialService = readingMaterialService;
        }

        [HttpGet("ReadingMaterials")]
        public async Task<IActionResult> GetReadingMaterialByEpisodeId(int episodeId)
        {
            
            var result = await _readingMaterialService.GetReadingMaterialByEpisodeId(episodeId);
            return Ok(result);
        }

    }
}

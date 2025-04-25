using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("Answers")]
        public async Task<IActionResult> GetAnswersByQuestionId(int questionId)
        {
            var result = await _answerService.GetAnswersByQuestionId(questionId);
            return Ok(result);
        }
    }

}

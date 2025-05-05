using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("Questions/{testId}")]
        public async Task<IActionResult> GetQuestionsByTestId(int testId)
        {            
            var result = await _questionService.GetQuestionsByTestId(testId);
            return Ok(result);
        }

        [HttpGet("AllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var result = await _questionService.GetAllQuestions();
            return Ok(result);
        }

    }
}

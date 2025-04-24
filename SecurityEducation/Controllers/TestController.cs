using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using SecurityEducation.Services.Interfaces;

namespace SecurityEducation.Controllers
{
    public class TestController : Controller
    {
        private readonly IQuestionService _questionService;

        public TestController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IActionResult> Questions()
        {
            var model = await _questionService.GetQuestionsByTestId(1); //obs
            return View(model);
        }
    }
}

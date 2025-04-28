using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using SecurityEducation.Services.Interfaces;

namespace SecurityEducation.Controllers
{
    public class TestController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;
        

        public TestController(IQuestionService questionService, ITestService testService)
        {
            _questionService = questionService;
            _testService = testService;            
        }
                

        [HttpGet("/Test/Questions/{id}")]
		public async Task<IActionResult> Questions(int id)        
        {
            
            var model = await _questionService.GetQuestionsByTestId(id);
            return View(model);
        }


		[HttpGet("/Test/TestStartPage/{id}")]
		public async Task<IActionResult> TestStartPage(int id)
        {
            var model = await _testService.GetTestInfoByEpisodeId(id);
            return View(model);
        }
		public async Task<IActionResult> Result()
		{
			return View();
		}
	}
}

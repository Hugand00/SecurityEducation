using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly ITestService _testService;

		public TestController(ITestService testService)
		{
			_testService = testService;
		}

		[HttpGet("TestInfo")]
		public async Task<IActionResult> GetTestInfo(int id)
		{
			var result = await _testService.GetTestInfoFromID(id);
			return Ok(result);
		}
	}
}

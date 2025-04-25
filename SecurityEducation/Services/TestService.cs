using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
	public class TestService : ITestService
	{
		private readonly ApiEngine _apiEngine;

		public TestService(ApiEngine apiEngine)
		{
			_apiEngine = apiEngine;
		}

		public async Task<TestViewModel> GetTestInfoByEpisodeId(int episodeId)
		{
			TestViewModel viewModel = new TestViewModel();
			var testData = await _apiEngine.GetAsync<TestDto>($"https://localhost:7215/api/Test/TestInfo?id={episodeId}");
			viewModel.TestInfo = testData;
			return viewModel;
		}
	}
}

using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
	public class TestService : ITestService
	{
		private readonly ApiEngine _apiEngine;
		private readonly IChapterService _chapterService;
		private readonly IEpisodeService _episodeService;
		private readonly string _baseUrl;

		public TestService(ApiEngine apiEngine, IChapterService chapterService, IEpisodeService episodeService, IConfiguration configuration)
		{
			_baseUrl = configuration["ApiBaseUrl"];
			_apiEngine = apiEngine;
			_chapterService = chapterService;
			_episodeService = episodeService;
		}

		public async Task<TestViewModel> GetTestInfoByEpisodeId(int episodeId)
		{
			TestViewModel viewModel = new TestViewModel();
			var testData = await _apiEngine.GetAsync<TestDto>($"{_baseUrl}api/Test/TestInfo?id={episodeId}");
			viewModel.TestInfo = testData;

			
			return viewModel;
		}
		

		
	}
}

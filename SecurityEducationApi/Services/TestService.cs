using SecurityEducationApi.Dtos;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{
	public class TestService : ITestService
	{
		private readonly ITestRepository _testRepository;

		public TestService(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		public async Task<TestDto> GetTestInfoFromID(int id)
		{
			var testData = await _testRepository.GetTestInfo(id);
			var testDto = new TestDto
			{
				Id = testData.Id,
				Name = testData.Name,
				Description = testData.Description,
				EpisodeId = testData.EpisodeId,
			};
			return testDto;
		}

	}
}

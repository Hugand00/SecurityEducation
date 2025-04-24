using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
	public interface ITestService
	{
		Task<TestViewModel> GetTestInfoByEpisodeId(int episodeId);
	}
}

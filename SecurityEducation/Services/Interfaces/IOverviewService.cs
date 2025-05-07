using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
	public interface IOverviewService
	{
		Task<OverviewViewModel> GetChaptersAndEpisodes();
	}
}

using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
	public class OverviewService : IOverviewService
	{
		private readonly IChapterService _chapterService;
		private readonly IEpisodeService _episodeService;
		private readonly string _baseUrl;

		public OverviewService( IChapterService chapterService, IEpisodeService episodeService,IConfiguration configuration)
		{
			_baseUrl = configuration["ApiBaseUrl"];
			_chapterService = chapterService;
			_episodeService = episodeService;
		}

		public async Task<OverviewViewModel> GetChaptersAndEpisodes()
		{
			OverviewViewModel viewModel = new OverviewViewModel();

			var chapterData = await _chapterService.GetEveryChapter();
			
			foreach (var chapter in chapterData.Chapters) 
			{
				viewModel.Chapters.Add(chapter);
				var episodeData = await _episodeService.GetEpisodesByChapterId(chapter.Id);
				foreach (var episodes in episodeData.Episodes)
				{
					viewModel.Episodes.Add(episodes);
				}
			}
			return viewModel;
		}
	}
}

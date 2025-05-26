using Microsoft.AspNetCore.SignalR;
using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
	public class ChapterService : IChapterService
	{
		
		private readonly string _baseUrl;
		private readonly ApiEngine _apiEngine;
		private readonly IEpisodeService _episodeService;

		public ChapterService(ApiEngine apiEngine, IEpisodeService episodeService, IConfiguration configuration)
		{
			_baseUrl = configuration["ApiBaseUrl"];
			_apiEngine = apiEngine;
			_episodeService = episodeService;
		}

		public async Task<ChapterViewModel> GetEveryChapter()
		{
			ChapterViewModel chapterViewModel = new ChapterViewModel();
			var chapters = await _apiEngine.GetAsync<ICollection<ChapterDto>>($"{_baseUrl}api/Chapter/Chapters");
			
			foreach (var chapter in chapters)
			{
				var numberOfEpisodes = _episodeService.GetEpisodesByChapterId(chapter.Id);
				chapter.NumberOfEpisodes = numberOfEpisodes.Result.Episodes.Count;
				chapterViewModel.Chapters.Add(chapter);
			}
			return chapterViewModel;
		}
		public async Task<ChapterDto> GetChapterFromId(int chapterId)
		{
			var chapters = await _apiEngine.GetAsync<ICollection<ChapterDto>>($"{_baseUrl}api/Chapter/Chapters");
			foreach (var chapter in chapters)
			{
				if(chapter.Id == chapterId)
				{
                    var numberOfEpisodes = _episodeService.GetEpisodesByChapterId(chapter.Id);
                    chapter.NumberOfEpisodes = numberOfEpisodes.Result.Episodes.Count;
                    return chapter;
                }
					
			}
			return null;

		}

	}
}

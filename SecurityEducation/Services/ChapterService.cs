using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
	public class ChapterService : IChapterService
	{
		private readonly ApiEngine _apiEngine;

		public ChapterService(ApiEngine apiEngine)
		{
			_apiEngine = apiEngine;
		}

		public async Task<ChapterViewModel> GetEveryChapter()
		{
			ChapterViewModel chapterViewModel = new ChapterViewModel();
			var chapters = await _apiEngine.GetAsync<ICollection<ChapterDto>>("https://localhost:7215/api/Chapter/Chapters");
			foreach (var chapter in chapters)
			{
				chapterViewModel.Chapters.Add(chapter);
			}
			return chapterViewModel;
		}

	}
}

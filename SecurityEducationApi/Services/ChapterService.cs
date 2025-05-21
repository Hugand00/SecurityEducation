using SecurityEducationApi.Dtos;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{
	public class ChapterService : IChapterService
	{
		private readonly IChapterRepository _chapterRepository;

		public ChapterService(IChapterRepository chapterRepository)
		{
			_chapterRepository = chapterRepository;
		}

		public async Task<List<ChapterDto>> GetEveryChapter()
		{
			var chapters = await _chapterRepository.GetEveryChapter();
			return chapters.Select(c => new ChapterDto
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				ImageUrl = c.ImageUrl,
				ImageAltText = c.ImageAltText,
				EstimatedTime = c.EstimatedTime,
			}).ToList();
		}
	}
}

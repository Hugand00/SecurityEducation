using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
	public interface IChapterService
	{
		Task<List<ChapterDto>> GetEveryChapter();
	}
}

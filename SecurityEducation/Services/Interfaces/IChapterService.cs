using SecurityEducation.Dtos;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
	public interface IChapterService
	{
		Task<ChapterDto> GetChapterFromId(int chapterId);
		Task<ChapterViewModel> GetEveryChapter();
	}
}

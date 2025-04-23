using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
	public interface IChapterService
	{
		Task<ChapterViewModel> GetEveryChapter();
	}
}

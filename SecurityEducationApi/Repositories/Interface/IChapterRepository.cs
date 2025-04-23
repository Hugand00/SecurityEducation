
using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
	public interface IChapterRepository
	{
		Task<List<Chapter>> GetEveryChapter();
	}
}

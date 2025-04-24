using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
    public interface IEpisodeRepository
    {
        Task<List<Episode>> GetEpisodesByChapterId(int chapterId);
    }
}

using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
    public interface IReadingMaterialRepository
    {
        Task<List<ReadingMaterial>> GetReadingMaterialByEpisodeId(int episodeId);
    }
}

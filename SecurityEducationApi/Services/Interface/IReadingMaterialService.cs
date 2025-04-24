
using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
    public interface IReadingMaterialService
    {
        Task<List<ReadingMaterialDto>> GetReadingMaterialByEpisodeId(int episodeId);
    }
}

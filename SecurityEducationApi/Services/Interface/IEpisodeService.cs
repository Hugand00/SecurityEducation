using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
    public interface IEpisodeService
    {
        Task<List<EpisodeDto>> GetEpisodesByChapterId(int chapterId);
    }
}

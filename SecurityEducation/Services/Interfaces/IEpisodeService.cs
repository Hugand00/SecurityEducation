using SecurityEducation.Dtos;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
    public interface IEpisodeService
    {
		Task<EpisodeDto> GetEpisodeById(int episodeId, int chapterId);
		Task<EpisodeViewModel> GetEpisodesByChapterId(int id);
    }
}

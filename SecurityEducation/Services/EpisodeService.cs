using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly ApiEngine _apiEngine;

        public EpisodeService(ApiEngine apiEngine)
        {
            _apiEngine = apiEngine;
        }

        public async Task<EpisodeViewModel> GetEpisodesByChapterId()
        {
            EpisodeViewModel episodeViewModel = new EpisodeViewModel();
            var episodes = await _apiEngine.GetAsync<ICollection<EpisodeDto>>("https://localhost:7215/api/Episode/Episodes?chapterId=1");
            foreach (var episode in episodes)
            {
                episodeViewModel.Episodes.Add(episode);
            }
            return episodeViewModel;
        }
    }
}

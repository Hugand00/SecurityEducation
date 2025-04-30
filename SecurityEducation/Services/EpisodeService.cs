using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("Episode/Episodes/{id}")]
        public async Task<EpisodeViewModel> GetEpisodesByChapterId(int id)
        {
            EpisodeViewModel episodeViewModel = new EpisodeViewModel();
            var episodes = await _apiEngine.GetAsync<ICollection<EpisodeDto>>($"https://localhost:7215/api/Episode/Episodes?chapterId={id}");
            foreach (var episode in episodes)
            {
                episodeViewModel.Episodes.Add(episode);
            }
            return episodeViewModel;
        }
		public async Task<EpisodeDto> GetEpisodeById(int episodeId, int chapterId)
        {
			var episodes = await _apiEngine.GetAsync<ICollection<EpisodeDto>>($"https://localhost:7215/api/Episode/Episodes?chapterId={chapterId}");
            foreach(var episode in episodes)
            {
                if (episode.Id == episodeId)
                    return episode;
            }
            return null;
		}
    }
}

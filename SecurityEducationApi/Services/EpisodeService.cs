using SecurityEducationApi.Dtos;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{   
   public class EpisodeService : IEpisodeService
   {
       private readonly IEpisodeRepository _episodeRepository;

       public EpisodeService(IEpisodeRepository episodeRepository)
       {
           _episodeRepository = episodeRepository;
       }

       public async Task<List<EpisodeDto>> GetEpisodesByChapterId(int chapterId)
       {
          var episodes = await _episodeRepository.GetEpisodesByChapterId(chapterId);
          return episodes.Select(e => new EpisodeDto
          {
              Id = e.Id,
              Name = e.Name,
              Description = e.Description,
              ImageUrl = e.ImageUrl, 
              ImageAltText = e.ImageAltText,
              ChapterId = e.ChapterId,
              EstimatedTime = e.EstimatedTime,
          }).ToList();
       }
   }
    
}

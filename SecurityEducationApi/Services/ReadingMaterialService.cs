using SecurityEducationApi.Dtos;
using SecurityEducationApi.Repositories;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{
    public class ReadingMaterialService : IReadingMaterialService
    {
        private readonly IReadingMaterialRepository _readingMaterialRepository;

        public ReadingMaterialService(IReadingMaterialRepository readingMaterialRepository)
        {
            _readingMaterialRepository = readingMaterialRepository;
        }

        public async Task<List<ReadingMaterialDto>> GetReadingMaterialByEpisodeId(int episodeId)
        {
            var readingMaterials = await _readingMaterialRepository.GetReadingMaterialByEpisodeId(episodeId);
            return readingMaterials.Select(r => new ReadingMaterialDto
            {
                Id = r.Id,
                Name = r.Name,
                Content = r.Content,
                ImageUrl = r.ImageUrl,
                PageNumber = r.PageNumber,
            }).ToList();
        }
    }
}

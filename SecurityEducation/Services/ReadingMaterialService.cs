using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
    public class ReadingMaterialService : IReadingMaterialService
    {
        private readonly ApiEngine _apiEngine;

        public ReadingMaterialService(ApiEngine apiEngine)
        {
            _apiEngine = apiEngine;
        }

        public async Task<ReadingMaterialViewModel> GetReadingMaterialByEpisodeId()
        {
            ReadingMaterialViewModel readingMaterialViewModel = new ReadingMaterialViewModel();
            var readingMaterials = await _apiEngine.GetAsync<ICollection<ReadingMaterialDto>>("https://localhost:7215/api/ReadingMaterial/ReadingMaterials");
            foreach (var readingMaterial in readingMaterials)
            {
                readingMaterialViewModel.ReadingMaterials.Add(readingMaterial);
            }
            return readingMaterialViewModel;
        }
    }
}

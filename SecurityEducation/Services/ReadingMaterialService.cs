using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
    public class ReadingMaterialService : IReadingMaterialService
    {
        private readonly string _baseUrl;
        private readonly ApiEngine _apiEngine;

        public ReadingMaterialService(ApiEngine apiEngine, IConfiguration configuration)
        {
			_baseUrl = configuration["ApiBaseUrl"];
			_apiEngine = apiEngine;
        }


		[HttpGet("/ReadingMaterial/ReadingMaterials/{id}")]
		public async Task<ReadingMaterialViewModel> GetReadingMaterialByEpisodeId(int id)
        {
            ReadingMaterialViewModel readingMaterialViewModel = new ReadingMaterialViewModel();
            var readingMaterials = await _apiEngine.GetAsync<ICollection<ReadingMaterialDto>>($"{_baseUrl}api/ReadingMaterial/ReadingMaterials?episodeId={id}");
            foreach (var readingMaterial in readingMaterials)
            {
                readingMaterialViewModel.ReadingMaterials.Add(readingMaterial);
            }
            return readingMaterialViewModel;
        }
    }
}

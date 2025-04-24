using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
    public interface IReadingMaterialService
    {
        Task<ReadingMaterialViewModel> GetReadingMaterialByEpisodeId(int id);
    }
}

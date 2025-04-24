using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
    public class ReadingMaterialViewModel
    {
        public ICollection<ReadingMaterialDto> ReadingMaterials { get; set; } = new List<ReadingMaterialDto>();
    }
}

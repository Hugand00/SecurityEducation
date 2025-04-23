using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
    public class EpisodeViewModel
    {
        public ICollection<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
    }
}

using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
	public class OverviewViewModel
	{
		public ICollection<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
		public ICollection<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
	}
}

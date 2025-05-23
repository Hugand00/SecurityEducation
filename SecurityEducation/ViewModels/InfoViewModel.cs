using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
	public class InfoViewModel
	{
		public ChapterDto Chapter { get; set; }
		public EpisodeDto Episode { get; set; }

		public ICollection<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
		public ICollection<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
	}
}

using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
	public class ChapterViewModel
	{
		public ICollection<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
		
	}
}

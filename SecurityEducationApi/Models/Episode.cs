using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityEducationApi.Models
{
	public class Episode
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string? ImageUrl { get; set; }
		public string ImageAltText { get; set; }
		public int ChapterId { get; set; }
		public Chapter Chapter { get; set; }		
		public Test Test { get; set; }
		public string EstimatedTime { get; set; }
	}
}	
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityEducationApi.Models
{
	public class Test
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int EpisodeId { get; set; }
		[ForeignKey("EpisodeId")]
		public Episode Episode { get; set; }
		public ICollection<Question> Questions { get; set; }
	}
}

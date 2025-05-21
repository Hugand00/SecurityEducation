namespace SecurityEducationApi.Models
{
	public class Chapter
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string  Description { get; set; }
		public string ImageUrl { get; set; }
		public string ImageAltText { get; set; }
		public ICollection<Episode> Episodes { get; set; }
		public string EstimatedTime { get; set; }
	}
}

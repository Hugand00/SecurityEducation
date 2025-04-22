namespace SecurityEducationApi.Models
{
	public class Episode
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Chapter chapter { get; set; }
		public Test Test { get; set; }
	}
}	
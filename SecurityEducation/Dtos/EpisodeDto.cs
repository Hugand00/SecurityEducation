namespace SecurityEducation.Dtos
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
		public string ImageAltText { get; set; }
		public int ChapterId { get; set; }
		public string EstimatedTime { get; set; }
	}
}

namespace SecurityEducationApi.Models
{
    public class ReadingMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int PageNumber {  get; set; }
        public string ImageUrl { get; set; }
		public string ImageAltText { get; set; }
		public int EpisodeId { get; set; }
        public Episode Episode { get; set; }

    }
}

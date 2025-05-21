namespace SecurityEducationApi.Dtos
{
    public class ReadingMaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
		public string ImageAltText { get; set; }
		public int PageNumber { get; set; }
        public int EpisodeId { get; set; }
    }
}

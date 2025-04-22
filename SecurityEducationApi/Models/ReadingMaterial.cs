namespace SecurityEducationApi.Models
{
    public class ReadingMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int SlideNr {  get; set; }
        public string ImageUrl { get; set; }
        public Episode Episode { get; set; }

    }
}

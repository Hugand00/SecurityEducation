namespace SecurityEducationApi.Models
{
	public class Question
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Test	Test { get; set; }
		public int TestId { get; set; }
		public ICollection<Answer> Answers { get; set; }
	}
}

using SecurityEducation.Dtos;

namespace SecurityEducation.ViewModels
{
    public class QuestionViewModel
    {
        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public ICollection<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    }
}

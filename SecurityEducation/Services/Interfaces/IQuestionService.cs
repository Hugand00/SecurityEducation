using SecurityEducation.ViewModels;

namespace SecurityEducation.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<QuestionViewModel> GetAllQuestions();
        Task<QuestionViewModel> GetQuestionsByTestId(int testId);
    }
}

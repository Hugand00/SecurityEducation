using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> GetAllQuestions();
        Task<List<QuestionDto>> GetQuestionsByTestId(int testId);
    }
}

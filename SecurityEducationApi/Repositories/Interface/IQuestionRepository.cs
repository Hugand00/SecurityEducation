using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestionsByTestId(int testId);
    }
}

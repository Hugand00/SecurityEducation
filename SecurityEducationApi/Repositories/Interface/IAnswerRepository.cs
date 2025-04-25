using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> GetAnswersByQuestionId(int questionId);
    }
}

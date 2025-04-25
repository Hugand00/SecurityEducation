using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
    public interface IAnswerService
    {
        Task<List<AnswerDto>> GetAnswersByQuestionId(int questionId);
    }
}

using SecurityEducationApi.Dtos;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<List<AnswerDto>> GetAnswersByQuestionId(int questionId)
        {
            var answers = await _answerRepository.GetAnswersByQuestionId(questionId);
            return answers.Select(a => new AnswerDto
            {
                Id = a.Id,
                Title = a.Title,
                IsCorrect = a.IsCorrect,
                QuestionId = a.QuestionId,
            }).ToList();
        }
    }
}

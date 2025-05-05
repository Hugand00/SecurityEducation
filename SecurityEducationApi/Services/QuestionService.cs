using SecurityEducationApi.Dtos;
using SecurityEducationApi.Repositories;
using SecurityEducationApi.Repositories.Interface;
using SecurityEducationApi.Services.Interface;

namespace SecurityEducationApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        
        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<List<QuestionDto>> GetQuestionsByTestId(int testId)
        {
            var questions = await _questionRepository.GetQuestionsByTestId(testId);
            return questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                TestId = q.TestId,
                
            }).ToList();
        }

        public async Task<List<QuestionDto>> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();
            return questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                TestId = q.TestId,

            }).ToList();
        }
    }
}

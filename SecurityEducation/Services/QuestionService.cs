using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApiEngine _apiEngine;
        private readonly string _baseUrl;

        public QuestionService(ApiEngine apiEngine, IConfiguration configuration)
        {
			_baseUrl = configuration["ApiBaseUrl"];
			_apiEngine = apiEngine;
        }
                
        public async Task<QuestionViewModel> GetQuestionsByTestId(int testId)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            var questions = await _apiEngine.GetAsync<ICollection<QuestionDto>>($"{_baseUrl}api/Question/Questions/{testId}");
             
            foreach (var question in questions)
            {
                questionViewModel.Questions.Add(question);
                var answers = await _apiEngine.GetAsync<ICollection<AnswerDto>>($"{_baseUrl}api/Answer/Answers?questionId={question.Id}");
                foreach (var answer in answers)
                {
                    questionViewModel.Answers.Add(answer);
                }
            }    
            
            return questionViewModel;
        }

        public async Task<QuestionViewModel> GetAllQuestions()
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            var questions = await _apiEngine.GetAsync<ICollection<QuestionDto>>($"{_baseUrl}api/Question/AllQuestions");

            foreach (var question in questions)
            {
                questionViewModel.Questions.Add(question);
                var answers = await _apiEngine.GetAsync<ICollection<AnswerDto>>($"{_baseUrl}api/Answer/Answers?questionId={question.Id}");
                foreach (var answer in answers)
                {
                    questionViewModel.Answers.Add(answer);
                }
            }

            return questionViewModel;
        }


    }
}

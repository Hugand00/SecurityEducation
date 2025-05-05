using Microsoft.AspNetCore.Mvc;
using SecurityEducation.Dtos;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApiEngine _apiEngine;

        public QuestionService(ApiEngine apiEngine)
        {
            _apiEngine = apiEngine;
        }
                
        public async Task<QuestionViewModel> GetQuestionsByTestId(int testId)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            var questions = await _apiEngine.GetAsync<ICollection<QuestionDto>>($"https://localhost:7215/api/Question/Questions/{testId}");
             
            foreach (var question in questions)
            {
                questionViewModel.Questions.Add(question);
                var answers = await _apiEngine.GetAsync<ICollection<AnswerDto>>($"https://localhost:7215/api/Answer/Answers?questionId={question.Id}");
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
            var questions = await _apiEngine.GetAsync<ICollection<QuestionDto>>($"https://localhost:7215/api/Question/AllQuestions");

            foreach (var question in questions)
            {
                questionViewModel.Questions.Add(question);
                var answers = await _apiEngine.GetAsync<ICollection<AnswerDto>>($"https://localhost:7215/api/Answer/Answers?questionId={question.Id}");
                foreach (var answer in answers)
                {
                    questionViewModel.Answers.Add(answer);
                }
            }

            return questionViewModel;
        }


    }
}

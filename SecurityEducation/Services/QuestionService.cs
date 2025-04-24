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
            var questions = await _apiEngine.GetAsync<ICollection<QuestionDto>>($"https://localhost:7215/api/Question/Questions?testId={testId}");
            foreach (var question in questions)
            {
                questionViewModel.Questions.Add(question);
            }
            return questionViewModel;
        }


    }
}

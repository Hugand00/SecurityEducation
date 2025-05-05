using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsByTestId(int testId)
        {
            return await _context.Questions
                                 .Where(q => q.TestId == testId)
                                 .ToListAsync();
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

    }
}

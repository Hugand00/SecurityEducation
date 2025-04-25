using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _context.Answers
                                 .Where(a => a.QuestionId == questionId)
                                 .ToListAsync();
        }
    }
}

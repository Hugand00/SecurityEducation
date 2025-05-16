using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
	public class ChapterRepository : IChapterRepository
	{
		private readonly AppDbContext _context;

		public ChapterRepository(AppDbContext context)
		{
			_context = context;
		}

        public async Task<List<Chapter>> GetEveryChapter()
        {
            return await _context.Chapters
                .OrderBy(c => c.Id)
                .ToListAsync();
        }
    }
}

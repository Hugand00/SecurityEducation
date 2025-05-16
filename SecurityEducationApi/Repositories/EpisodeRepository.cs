using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Episode>> GetEpisodesByChapterId(int chapterId)
        {
            return await _context.Episodes
                                 .Where(e => e.ChapterId == chapterId)
                                 .OrderBy(e => e.Id)
                                 .ToListAsync();
        }
    }

}

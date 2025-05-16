using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;
using System.Runtime.CompilerServices;

namespace SecurityEducationApi.Repositories
{
    public class ReadingMaterialRepository : IReadingMaterialRepository
    {
        private readonly AppDbContext _context;

        public ReadingMaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReadingMaterial>> GetReadingMaterialByEpisodeId(int episodeId)
        {
            return await _context.ReadingMaterials
                                 .Where(r => r.EpisodeId == episodeId)
                                 .OrderBy(r => r.Id)
                                 .ToListAsync();
        }

    }
}

using SecurityEducationApi.Data;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
	public class GradingRepository : IGradingRepository
	{
		private readonly AppDbContext _context;

		public GradingRepository(AppDbContext context)
		{
			_context = context;
		}


	}
}

using SecurityEducationApi.Data;
using SecurityEducationApi.Models;
using SecurityEducationApi.Repositories.Interface;

namespace SecurityEducationApi.Repositories
{
	public class TestRepository : ITestRepository
	{
		private readonly AppDbContext _context;

		public TestRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Test?> GetTestInfo(int id)
		{
			var testData = await _context.Tests.FindAsync(id);
			if (testData != null)
			{
				return testData;
			}
			return null;
			
		}
	}
}

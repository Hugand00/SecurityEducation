using Microsoft.EntityFrameworkCore;

namespace SecurityEducationApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SecurityEducationApi.Models;

namespace SecurityEducationApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Chapter> Chapters { get; set; }
		public DbSet<Episode> Episodes { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<ReadingMaterial> ReadingMaterials { get; set; }
	}
}

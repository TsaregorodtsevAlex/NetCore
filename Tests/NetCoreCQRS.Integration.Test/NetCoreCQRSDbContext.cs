using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Integration.Test.Model;

namespace NetCoreCQRS.Integration.Test
{
	public class NetCoreCQRSDbContext : DbContext
	{
		public NetCoreCQRSDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Point> Points { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Point>().HasKey(r => r.Id);

		}
	}
}

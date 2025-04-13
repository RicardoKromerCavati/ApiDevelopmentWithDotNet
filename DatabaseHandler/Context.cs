using DatabaseHandler.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHandler
{
	public class Context : DbContext
	{
		public Context(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DbGamer>().HasKey(g => g.Id);
			modelBuilder.Entity<DbGamer>().Property(g => g.Name).HasMaxLength(254);
			modelBuilder.Entity<DbGamer>().Property(g => g.Email).HasMaxLength(254);
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<DbGamer> Gamers { get; set; }
	}
}

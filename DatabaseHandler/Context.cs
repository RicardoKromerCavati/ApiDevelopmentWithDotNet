using DatabaseHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseHandler
{
	public class Context : DbContext
	{
		private readonly IConfiguration _configuration;
		public Context(IConfiguration configuration, DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			_configuration = configuration;
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

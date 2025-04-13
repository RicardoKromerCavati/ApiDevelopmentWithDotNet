using DatabaseHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHandler
{
	public class Context : DbContext
	{
		private readonly IConfiguration _configuration;
		public Context(IConfiguration configuration, DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			_configuration = configuration;
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	var connString =
		//	optionsBuilder.UseMySql(ServerVersion.AutoDetect(_configuration.GetConnectionString("MyWebApi")));
		//	base.OnConfiguring(optionsBuilder);
		//}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DbGamer>().HasKey(g => g.Id);
			modelBuilder.Entity<DbGamer>().Property(g => g.Name).HasMaxLength(254);
			modelBuilder.Entity<DbGamer>().Property(g => g.Email).HasMaxLength(254);
			base.OnModelCreating(modelBuilder);
		}
	}
}

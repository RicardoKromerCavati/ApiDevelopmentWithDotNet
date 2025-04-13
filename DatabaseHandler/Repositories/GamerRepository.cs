using Dapper;
using DatabaseHandler.Contracts.Repositories;
using DatabaseHandler.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DatabaseHandler.Repositories
{
	public class GamerRepository : IGamerRepository
	{
		private readonly Context _context;
		private readonly IConfiguration _configuration;

		public GamerRepository(Context context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public void Dapper_Create(DbGamer gamer)
		{
			var connectionString = GetConnectionString();

			using var connection = new SqliteConnection(connectionString);

			var insertQuery = "INSERT INTO Gamers (Name,Email,Password) VALUES (@Name,@Email,@Password)";

			connection.Execute(insertQuery, new { gamer.Name, gamer.Email, gamer.Password });
		}

		public DbGamer? Dapper_Read(string name)
		{
			var connectionString = GetConnectionString();

			using var connection = new SqliteConnection(connectionString);

			var selectQuery = "SELECT * FROM Gamers WHERE Name = @Name";

			return connection.QueryFirstOrDefault<DbGamer>(selectQuery, new { Name = name });
		}

		public void Dapper_Update(int id, string email)
		{
			var connectionString = GetConnectionString();

			using var connection = new SqliteConnection(connectionString);

			var updateQuery = "UPDATE Gamers SET Email = @Email WHERE Id = @Id";

			connection.Execute(updateQuery, new { Email = email, Id = id });
		}

		public void Dapper_Delete (int id)
		{
			var connectionString = GetConnectionString();

			using var connection = new SqliteConnection(connectionString);

			var deleteQuery = "DELETE FROM Gamers WHERE Id = @Id";

			connection.Execute(deleteQuery, new { Id = id });
		}

		private string? GetConnectionString()
		{
			const string connectionStringName = "MyWebApi";
			return _configuration.GetConnectionString(connectionStringName);
		}

		public void EF_Create(DbGamer gamer)
		{
			_context.Gamers.Add(gamer);
			_context.SaveChanges();
		}

		public void EF_Delete(int id)
		{
			var gamer = _context.Gamers.FirstOrDefault(g => g.Id == id);
			
			if (gamer == null)
			{
				return;
			}

			_context.Gamers.Remove(gamer);
			_context.SaveChanges();
		}

		public DbGamer? EF_Read(string name) =>
			_context.Gamers.FirstOrDefault(g => g.Name.Equals(name));

		public void EF_Update(int id, string email)
		{
			var gamer = _context.Gamers.FirstOrDefault(g => g.Id == id);

			if (gamer == null)
			{
				return;
			}

			gamer.Email = email;

			_context.SaveChanges();
		}

		public IEnumerable<DbGamer> SelectAll() => _context.Gamers;
	}
}

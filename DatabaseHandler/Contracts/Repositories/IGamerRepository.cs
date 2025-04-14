using DatabaseHandler.Models;

namespace DatabaseHandler.Contracts.Repositories;

public interface IGamerRepository
{
    void EF_Create(DbGamer gamer);
	DbGamer? EF_Read(string name);
	void EF_Update(int id, string email);
	void EF_Delete(int id);

	void Dapper_Create(DbGamer gamer);
	DbGamer? Dapper_Read(string name);
	void Dapper_Update(int id, string email);
	bool Dapper_Delete(int id);

	IEnumerable<DbGamer> SelectAll();
}
using DatabaseHandler.Models;

namespace DatabaseHandler.Contracts.Repositories;

public interface IGamerRepository
{
    void EF_Create(DbGamer gamer);
}
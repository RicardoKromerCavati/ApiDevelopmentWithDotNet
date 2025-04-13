using Common.Models;
using DatabaseHandler.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamerController : Controller
{
    private readonly IGamerRepository _gamerRepository;
    [HttpPut]
    public IActionResult Index(string name, string email, string password)
    {
        var gamer = new Gamer(name, email, password);
        _gamerRepository.EF_Create(gamer);
        return Ok("Created Gamer");
    }
}
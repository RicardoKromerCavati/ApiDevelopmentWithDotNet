using Common.Models;
using DatabaseHandler.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamerController : Controller
{
	private readonly IGamerRepository _gamerRepository;

	public GamerController(IGamerRepository gamerRepository)
	{
		_gamerRepository = gamerRepository;
	}

	[HttpPost("PostViaEf")]
	public IActionResult PostViaEf(string name, string email, string password)
	{
		var gamer = new Gamer(name, email, password);
		_gamerRepository.EF_Create(gamer);
		return Ok("Created Gamer");
	}

	[HttpGet("GetViaEf")]
	public IActionResult GetViaEf(string name)
	{
		var gamer = _gamerRepository.EF_Read(name);
		
		if (gamer is null)
		{
			return NotFound();
		}

		return Ok(gamer);
	}

	[HttpPatch("PatchEmailViaEF")]
	public IActionResult PatchViaEF(int id, string email)
	{
		_gamerRepository.EF_Update(id, email);
		return Ok("Success");
	}

	[HttpDelete("DeleteEmailViaEF")]
	public IActionResult DeleteEmailViaEf(int id)
	{
		_gamerRepository.Dapper_Delete(id);
		return Ok("Success");
	}

	[HttpPost("PostViaDapper")]
	public IActionResult PostViaDapper(string name, string email, string password)
	{
		var gamer = new Gamer(name, email, password);
		_gamerRepository.Dapper_Create(gamer);
		return Ok("Created Gamer");
	}

	[HttpGet("GetViaDapper")]
	public IActionResult GetViaDapper(string name)
	{
		var gamer = _gamerRepository.Dapper_Read(name);

		if (gamer is null)
		{
			return NotFound();
		}

		return Ok(gamer);
	}

	[HttpPatch("PatchEmailViaDapper")]
	public IActionResult PatchViaDapper(int id, string email)
	{
		_gamerRepository.Dapper_Update(id, email);
		return Ok("Success");
	}

	[HttpDelete("DeleteEmailViaDapper")]
	public IActionResult DeleteEmailViaDapper(int id)
	{
		_gamerRepository.Dapper_Delete(id);
		return Ok("Success");
	}

	[HttpGet]
	public IActionResult Get()
	{
		var gamers = _gamerRepository.SelectAll();
		return Ok(gamers);
	}
}
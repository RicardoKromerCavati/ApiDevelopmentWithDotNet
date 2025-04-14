using Common.Models;
using DatabaseHandler.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

	[SwaggerOperation(
		Description = "This endpoint receives an int identifier and deletes from database based on it.",
		OperationId = "{E9BBD6FB-B4AF-4376-ABD9-9D28807F62E6}",
		Summary = "Delete gamer from database based on id")]
	[SwaggerResponse(StatusCodes.Status200OK)]
	[SwaggerResponse(StatusCodes.Status404NotFound)]
	[Consumes("application/json")]
	[Produces("application/json")]
	[HttpDelete("DeleteByIdViaEf")]
	public IActionResult DeleteByIdViaEf(int id)
	{
		var result = _gamerRepository.Dapper_Delete(id);
		
		if (result)
		{
			return Ok("Success");
		}

		return NotFound("Id not found");
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

	[HttpDelete("DeleteByIdViaDapper")]
	public IActionResult DeleteByIdViaDapper(int id)
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
using Common.Models;
using DatabaseHandler.Contracts.Repositories;
using SQLitePCL;

namespace MyWebApi.Extensions;

public static class GamerEndpoints
{
    public static void ConfigureMinimalApiEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGet("/", new Func<object>(() => throw new Exception("error handling middleware example")));

        webApplication.MapGet("/Test",
            async (ILogger<Program> logger, HttpResponse httpResponse) =>
            {
                logger.LogInformation("Log test in Program");
                await httpResponse.WriteAsync("Test OK");
            });
        
        webApplication.MapPost("minimal-api/CreateGamer", CreateGamer);
    }

    private static IResult CreateGamer(Gamer gamer, IGamerRepository gamerRepository)
    {
        gamerRepository.EF_Create(gamer);
        //return TypedResults.Ok(gamer);
        return Results.Ok();
    }
}
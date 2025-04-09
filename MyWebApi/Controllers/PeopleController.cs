using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Controllers;

public class PeopleController(
    ILogger<PeopleController> logger,
    ILoggerFactory loggerFactory) : Controller
{
    private readonly Person[] _people =
    [
        new("John", "Doe@gmail.com"),
        new("Jane", "Doe@gmail.com"),
        new("Jack", "Doe@gmail.com"),
        new("Jacob", "Doe@gmail.com"),
        new("George", null!),
    ];

    [HttpGet("api/people")]
    public IActionResult GetPeople()
    {
        var sampleLogger = loggerFactory.CreateLogger("SampleLogger");

        sampleLogger.LogInformation(1000, "[{DT}] Obtaining people", DateTime.UtcNow.ToLongTimeString());

        logger.LogInformation("[{DT}] Obtaining people", DateTime.UtcNow.ToLongTimeString());
        logger.Log(LogLevel.Information, "{DT} Obtained people", DateTime.UtcNow.ToLongTimeString());
        logger.LogInformation("[{DT}] People obtained successfully", DateTime.UtcNow.ToLongTimeString());

        return Ok(_people);
    }

    [HttpGet("api/people/email")]
    public IActionResult GetPeopleEmails()
    {
        var emails = new List<string>();

        foreach (var person in _people)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(person.Email))
                {
                    throw new ArgumentNullException(nameof(person.Email), "Test exception:");
                }
                
                emails.Add(person.Email);
            }
            catch (Exception e)
            {
                logger.LogWarning(2000, e, "Email not found for person {Name}", person.Name);
            }
        }
        
        return Ok(emails);
    }
}
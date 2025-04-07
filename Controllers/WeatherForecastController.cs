using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using MyAPI.Services.Contracts;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILifecycleService _lifecycleService;
        private readonly LifecycleService2 _lifecycleService2;

		private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, ILifecycleService lifecycleService, LifecycleService2 lifecycleService2)
		{
			_logger = logger;
			_lifecycleService = lifecycleService;
			_lifecycleService2 = lifecycleService2;
		}

		//[HttpGet(Name = "GetWeatherForecast")]
  //      public IEnumerable<WeatherForecast> Get()
  //      {
  //          return Enumerable.Range(1, 5).Select(index => new WeatherForecast
  //          {
  //              Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
  //              TemperatureC = Random.Shared.Next(-20, 55),
  //              Summary = Summaries[Random.Shared.Next(Summaries.Length)]
  //          })
  //          .ToArray();
  //      }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new List<DateTime>();

			result.Add(_lifecycleService.Now());
            result.Add(_lifecycleService2.Now());

            return Ok(result);
        }
    }
}

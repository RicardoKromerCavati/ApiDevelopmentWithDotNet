using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyWebApi.Middlewares
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class LogMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LogMiddleware> _logger;

		public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public Task Invoke(HttpContext httpContext)
		{
			_logger.LogInformation("{DT} Passing through middleware", DateTime.UtcNow.ToLongTimeString());
			return _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	// public static class LogMiddlewareExtensions
	// {
	// 	public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
	// 	{
	// 		return builder.UseMiddleware<LogMiddleware>();
	// 	}
	// }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyAPI
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class Log2Middleware
	{
		private readonly RequestDelegate _next;

		public Log2Middleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{

			return _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class Log2MiddlewareExtensions
	{
		public static IApplicationBuilder UseLog2Middleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<Log2Middleware>();
		}
	}
}

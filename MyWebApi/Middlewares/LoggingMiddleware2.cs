namespace MyWebApi.Middlewares;

public class LoggingMiddleware2(RequestDelegate next, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger("InformationHandling");

    public async Task Invoke(HttpContext httpContext)
    {
        _logger.LogInformation("Handling request: {Path}", httpContext.Request.Path);

        await next(httpContext);

        _logger.LogInformation("Finished handling request.");
    }
}
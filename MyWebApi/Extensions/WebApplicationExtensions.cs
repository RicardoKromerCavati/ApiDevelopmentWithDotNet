using MyWebApi.Middlewares;

namespace MyWebApi.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder CreateMiddlewares(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<LoggingMiddleware2>()
            .UseMiddleware<ErrorHandlingMiddleware>();
        //app.UseMiddleware<LogMiddleware>();
    }
}
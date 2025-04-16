using MyWebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDependencyInjection();

var app = builder.Build();
    
app.ConfigureMinimalApiEndpoints();

//This is a minimal api middleware example
app.Use(async (_, next) => { await next(); });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(redocOptions =>
    {
        redocOptions.DocumentTitle = "My API documentation in REDOC";
        redocOptions.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseAuthorization();

app.MapControllers();

app.CreateMiddlewares();

app.Logger.LogInformation("{DateTime} Application configured successfully", DateTime.UtcNow.ToLongTimeString());

app.Run();
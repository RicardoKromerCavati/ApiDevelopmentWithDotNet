using MyWebApi.Extensions;
using MyWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDependencyInjection();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Test",
	async (ILogger<Program> logger, HttpResponse httpResponse) =>
	{
		logger.LogInformation("Log test in Program");
		await httpResponse.WriteAsync("Test OK");
	});

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

app.UseLogMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("{DateTime} Application configured successfully", DateTime.UtcNow.ToLongTimeString());

app.Run();
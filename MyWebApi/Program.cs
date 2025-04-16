using MyWebApi.Extensions;
using MyWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDependencyInjection();

var app = builder.Build();

app.ConfigureMinimalApiEndpoints();

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
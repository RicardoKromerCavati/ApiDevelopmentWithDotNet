using MyAPI;
using MyAPI.Services;
using MyAPI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Dependency Injection Configuration

builder.Services.AddTransient<ILifecycleService, LifecycleService>();
builder.Services.AddTransient<LifecycleService2>();

#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Test",
    async (ILogger<Program> logger, HttpResponse httpResponse) =>
    {
        logger.LogInformation("Log test in Program");
        await httpResponse.WriteAsync("Test OK");
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseLogMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("{DateTime} Application configured successfully", DateTime.UtcNow.ToLongTimeString());

app.Run();
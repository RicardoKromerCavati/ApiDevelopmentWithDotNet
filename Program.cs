using MyAPI;
using MyAPI.Services;
using MyAPI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseLogMiddleware();
app.UseLog2Middleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

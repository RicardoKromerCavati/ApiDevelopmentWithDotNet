using Common.Models;
using DatabaseHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAPI;
using MyAPI.Services;
using MyAPI.Services.Contracts;
using MySqlConnector;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
{
	var adminRole = Role.Admin.ToString();
	options.AddPolicy(adminRole, policy => policy.RequireRole(adminRole));
});

builder.Services.AddTransient<ILifecycleService, LifecycleService>();
builder.Services.AddTransient<LifecycleService2>();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Authorization:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authorization:Key"]))
		};
	});


var connectionString = builder.Configuration.GetConnectionString(nameof(MyWebApi));
var dbConnection = new MySqlConnection(connectionString);

builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddDbContext<Context>(options => options.UseMySql(ServerVersion.AutoDetect(dbConnection)));
//builder.Services.AddDbContext<Context>();


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
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseLogMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("{DateTime} Application configured successfully", DateTime.UtcNow.ToLongTimeString());

app.Run();
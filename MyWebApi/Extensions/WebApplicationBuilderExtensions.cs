using Common.Models;
using DatabaseHandler;
using DatabaseHandler.Contracts.Repositories;
using DatabaseHandler.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Services;
using MyAPI.Services.Contracts;
using MySqlConnector;
using System.Text;

namespace MyWebApi.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
		{
			ConfigureLogging(builder);
			ConfigureEndpoints(builder);
			ConfigureServices(builder);
			ConfigurePermissions(builder);
			ConfigureDatabase(builder);
		}

		private static void ConfigureLogging(WebApplicationBuilder builder)
		{
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
		}

		private static void ConfigureEndpoints(WebApplicationBuilder builder)
		{
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
		}

		private static void ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Services.AddTransient<ILifecycleService, LifecycleService>();
			builder.Services.AddTransient<LifecycleService2>();
		}

		private static void ConfigurePermissions(WebApplicationBuilder builder)
		{
			builder.Services.AddAuthorization(options =>
			{
				var adminRole = Role.Admin.ToString();
				options.AddPolicy(adminRole, policy => policy.RequireRole(adminRole));
			});

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
		}

		private static void ConfigureDatabase(WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration.GetConnectionString(nameof(MyWebApi));
			var dbConnection = new MySqlConnection(connectionString);

			builder.Services.AddScoped<DbContext, Context>();
			builder.Services.AddDbContext<Context>(options => options.UseSqlite(connectionString));
			builder.Services.AddScoped<DbContext, Context>();
			builder.Services.AddTransient<IGamerRepository, GamerRepository>();
		}
	}
}

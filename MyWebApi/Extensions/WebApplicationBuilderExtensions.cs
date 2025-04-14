using Common.Models;
using DatabaseHandler;
using DatabaseHandler.Contracts.Repositories;
using DatabaseHandler.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using MyWebApi.ApiDocumentation.ExampleRequests;
using MyWebApi.ApiDocumentation.SchemaFilters;
using MyWebApi.Services;
using MyWebApi.Services.Contracts;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace MyWebApi.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
		{
			ConfigureLogging(builder);
			ConfigureEndpoints(builder);
			ConfigureApiDocumentation(builder);
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

		}

		private static void ConfigureApiDocumentation(WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen(options =>
			{
				options.EnableAnnotations();

				//options.SwaggerDoc("v1", new OpenApiInfo
				//{
				//	Title = "My Web API Swagger Documentation",
				//	Description = "This is an example of how to document my API :)",
				//	Contact = new OpenApiContact() { Name = "Ricardo Kromer Cavati", Email = "sample@email.com" },
				//	License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") }
				//});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please input the secret api key",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				var securityScheme = new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } };

				options.AddSecurityRequirement(securityRequirement);

				options.ExampleFilters();
				options.SchemaFilter<EnumSchemaFilter>();
			});

			builder.Services.AddSwaggerExamplesFromAssemblyOf<DangerousAuthorizationModelExample>();
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

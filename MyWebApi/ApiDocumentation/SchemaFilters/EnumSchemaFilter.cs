using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyWebApi.ApiDocumentation.SchemaFilters
{
	public class EnumSchemaFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context is null || schema is null) { return; }

			if (context.Type.IsEnum)
			{
				var values = Enum.GetNames(context.Type);

				var modifiedSchema = new List<IOpenApiAny>();

				for (int i = 0; i < values.Length; i++)
				{
					string? value = values[i];
					modifiedSchema.Add(new OpenApiString($"{i} {value}"));
				}

				schema.Enum.Clear();
				schema.Enum = modifiedSchema;
			}
		}
	}
}

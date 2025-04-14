using Common.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MyWebApi.ApiDocumentation.ExampleRequests
{
	public class DangerousAuthorizationModelExample : IExamplesProvider<DangerousAuthorization>
	{
		public DangerousAuthorization GetExamples() =>
			new DangerousAuthorization("username", "domain");
	}
}

using MyWebApi.Services.Contracts;

namespace MyWebApi.Services;

public class LifecycleService : ILifecycleService
{
	private readonly DateTime _date = DateTime.Now;
	public DateTime Now()
	{
		return _date;
	}
}

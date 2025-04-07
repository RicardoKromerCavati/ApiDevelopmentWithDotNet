
using MyAPI.Services.Contracts;

namespace MyAPI.Services;

public class LifecycleService : ILifecycleService
{
	private readonly DateTime _date = DateTime.Now;
	public DateTime Now()
	{
		return _date;
	}
}

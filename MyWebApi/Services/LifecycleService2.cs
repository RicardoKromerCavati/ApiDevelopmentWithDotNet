
using MyAPI.Services.Contracts;

namespace MyAPI.Services;

public class LifecycleService2 : ILifecycleService
{
	private readonly ILifecycleService _lifecycleService;

	public LifecycleService2(ILifecycleService lifecycleService)
	{
		_lifecycleService = lifecycleService;
	}

	public DateTime Now() => _lifecycleService.Now();
}

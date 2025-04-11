using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SampleAuthorizationController : Controller
{
    [HttpGet("protected_sample")]
    [Authorize(Policy = "Admin")]
    public IActionResult AdminOnly() => Ok("You are an administrator");

    [HttpGet("sample")]
    [Authorize]
    public IActionResult AnyUser() => Ok("You are an authenticated user");
}
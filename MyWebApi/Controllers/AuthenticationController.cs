using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.ApiDocumentation.ExampleRequests;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController(IConfiguration configuration) : Controller
{
    private readonly User[] _users =
    [
        new("admin", "admin", Role.Admin),
        new("user", "user", Role.CommonUser)
    ];


    [HttpPost("login")]
    [SwaggerRequestExample(typeof(DangerousAuthorization), typeof(DangerousAuthorizationModelExample))]
    public IActionResult Login(DangerousAuthorization dangerousAuthorization)
    {
        var (username, password) = dangerousAuthorization;

        var dbUser = _users.SingleOrDefault(u => u.Username.Equals(username, StringComparison.Ordinal));

        if (dbUser == null)
        {
            return Unauthorized();
        }

        if (dbUser.Password.Equals(password) is false)
        {
            return Unauthorized();
        }

        var (createdSuccessfully, token) = GenerateToken(dbUser.Username, dbUser.Role);

        if (createdSuccessfully is false)
        {
            return Unauthorized();
        }
        
        return Ok(token);
    }

    private (bool, string) GenerateToken(string username, Role role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var keyStr = configuration["Authorization:Key"];

        if (string.IsNullOrWhiteSpace(keyStr))
        {
            return (false, string.Empty);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Authorization:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return (true, new JwtSecurityTokenHandler().WriteToken(token));
    }
}
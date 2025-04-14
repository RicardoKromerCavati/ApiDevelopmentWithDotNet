using System.Text.Json.Serialization;

namespace Common.Models;

public class DangerousAuthorization
{
    public DangerousAuthorization(string username, string password)
    {
        Username = username;
        Password = password;
    }

    [JsonPropertyName("username")]
    public string Username { get; init; }
    
    [JsonPropertyName("password")]
    public string Password { get; init; }

    public void Deconstruct(out string username, out string password)
    {
        username = Username;
        password = Password;
    }
}
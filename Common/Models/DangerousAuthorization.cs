using System.Text.Json.Serialization;

namespace Common.Models;

public class DangerousAuthorization
{
    public DangerousAuthorization(string username, string password)
    {
        Username = username;
        Password = password;
    }

    [JsonPropertyName("Username")]
    public string Username { get; init; }
    
    [JsonPropertyName("Password")]
    public string Password { get; init; }

    public void Deconstruct(out string username, out string password)
    {
        username = Username;
        password = Password;
    }
}
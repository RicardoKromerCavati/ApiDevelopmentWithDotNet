using DatabaseHandler.Models;

namespace Common.Models;

public class Gamer
{
    public Gamer(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public static implicit operator DbGamer(Gamer gamer) => 
        new(gamer.Name, gamer.Email, gamer.Password);
}
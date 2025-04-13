namespace DatabaseHandler.Models;

public class DbGamer
{
	public DbGamer()
	{
		
	}

	public DbGamer(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
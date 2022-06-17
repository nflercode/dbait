using DbaitArgue.Contexts;

namespace DbaitArgue.Queries.Models;

public class Auth
{
    public string AuthToken { get; set; } = "";
    public User User { get; set; } = new();
}
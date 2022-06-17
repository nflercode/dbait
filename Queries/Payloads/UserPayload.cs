using DbaitArgue.Queries.Models;

namespace DbaitArgue.Queries.Payloads;

public class UserPayload
{
    public User User { get; set; } = new();
}
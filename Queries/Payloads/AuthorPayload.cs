using DbaitArgue.Queries.Models;

namespace DbaitArgue.Queries.Payloads;

public class AuthorPayload
{
    public Author Author { get; set; } = new();
}

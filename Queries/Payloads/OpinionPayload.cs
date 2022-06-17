using DbaitArgue.Queries.Models;

namespace DbaitArgue.Queries.Payloads;

public class OpinionPayload
{
    public Opinion Opinion { get; set; } = new();
}
using DbaitArgue.Queries.Models;

namespace DbaitArgue.Queries.Payloads;

public class ResponsePayload
{
    public Response Response { get; set; } = new();
}

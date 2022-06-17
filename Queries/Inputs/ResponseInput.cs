namespace DbaitArgue.Queries.Inputs;

public class ResponseInput
{
    public int OpinionId { get; set; }

    [GraphQLNonNullType]
    public string Content { get; set; } = "";
}
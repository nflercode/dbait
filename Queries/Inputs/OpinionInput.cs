namespace DbaitArgue.Queries.Inputs;

public class OpinionInput
{

    [GraphQLNonNullType]
    public string Title { get; set; } = "";

    [GraphQLNonNullType]
    public string Content { get; set; } = "";
}
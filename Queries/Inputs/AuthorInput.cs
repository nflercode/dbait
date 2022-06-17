namespace DbaitArgue.Queries.Inputs;

public class AuthorInput
{
    [GraphQLNonNullType]
    public string Name { get; set; } = "";
}
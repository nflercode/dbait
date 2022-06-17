namespace DbaitArgue.Queries.Inputs;

public class UserInput
{
    [GraphQLNonNullType]
    public string Name { get; set; } = "";

    [GraphQLNonNullType]
    public string Password { get; set; } = "";

    public string Email { get; set; } = "";
}
namespace DbaitArgue.Queries.Models;

public class Opinion
{
    public int Id { get; set; }

    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public Author Author { get; set; } = new();
    public List<Response> Responses { get; set; } = new();
}
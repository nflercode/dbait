namespace DbaitArgue.Queries.Models;

public class Response
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int OpinionId { get; set; }

    public string Content { get; set; } = "";

    public Author Author { get; set; } = new();
}
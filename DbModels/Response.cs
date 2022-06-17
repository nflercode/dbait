namespace DbaitArgue.DbModels;

public class Response
{
    public int Id { get; set; }
    public string Content { get; set; } = "";

    public int AuthorId { get; set; }
    public Author Author { get; set; } = new();

    public int OpinionId { get; set; }
    public Opinion Opinion { get; set; } = new();
}
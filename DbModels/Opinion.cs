using System.Collections.ObjectModel;

namespace DbaitArgue.DbModels;

public class Opinion
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public int AuthorId { get; set; }
    public Author Author { get; set; } = new();

    public ICollection<Response> Responses { get; set; } = new Collection<Response>();
}
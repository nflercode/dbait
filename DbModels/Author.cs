using System.Collections.ObjectModel;

namespace DbaitArgue.DbModels;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Opinion> Opinions { get; set; } = new Collection<Opinion>();
    public ICollection<Response> Responses { get; set; } = new Collection<Response>();
}
using DbaitArgue.Contexts;
using DbaitArgue.Queries.Models;

namespace DbaitArgue.Queries;

public class Query
{
    [UseDbContext(typeof(DbaitDbContext))]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Opinion> GetOpinions([ScopedService] DbaitDbContext dbaitDbContext) =>
     dbaitDbContext.Opinions.Select(entity => new Opinion
     {
         Id = entity.Id,
         Title = entity.Title,
         Content = entity.Content,
         Author = new Author
         {
             Id = entity.Author.Id,
             Name = entity.Author.Name
         },
         Responses = entity.Responses.Select(r => new Response
         {
             Id = r.Id,
             Content = r.Content,
             Author = new Author
             {
                 Id = r.Author.Id,
                 Name = r.Author.Name
             }
         }).ToList()
     });
}
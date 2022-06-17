using GqlModels = DbaitArgue.Queries.Models;
using DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using DbaitArgue.Queries.Inputs;
using DbaitArgue.Contexts;

namespace DbaitArgue.Services;

public class OpinionService : IAsyncDisposable
{
    private readonly DbaitDbContext _dbaitDbContext;

    public OpinionService(IDbContextFactory<DbaitDbContext> dbaitDbContext)
    {
        _dbaitDbContext = dbaitDbContext.CreateDbContext();
    }

    public async Task<GqlModels.Opinion> Create(OpinionInput opinionInput, int userId)
    {
        var author = await _dbaitDbContext.Authors.SingleOrDefaultAsync(a => a.UserId == userId);
        if (author == null)
            throw new Exception("Author does not exist");

        var newOpinion = new DbModels.Opinion
        {
            Id = 0,
            Title = opinionInput.Title,
            Content = opinionInput.Content,
            Author = author
        };

        var entry = _dbaitDbContext.Opinions.Add(newOpinion);
        await _dbaitDbContext.SaveChangesAsync();

        var entity = entry.Entity;
        return new GqlModels.Opinion
        {
            Id = entity.Id,
            Author = new Author
            {
                Id = entity.Author.Id,
                Name = entity.Author.Name
            },
            Title = entity.Title,
            Content = entity.Content
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _dbaitDbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
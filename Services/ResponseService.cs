using GqlModels = DbaitArgue.Queries.Models;
using DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using DbaitArgue.Queries.Inputs;
using DbaitArgue.Contexts;

namespace DbaitArgue.Services;

public class ResponseService : IAsyncDisposable
{
    private readonly DbaitDbContext _dbaitDbContext;

    public ResponseService(IDbContextFactory<DbaitDbContext> dbaitDbContext)
    {
        _dbaitDbContext = dbaitDbContext.CreateDbContext();
    }

    public async Task<GqlModels.Response> Create(ResponseInput responseInput, int userId)
    {
        var author = await _dbaitDbContext.Authors.SingleOrDefaultAsync(a => a.UserId == userId);
        if (author == null)
            throw new Exception("Author does not exist");

        var opinion = await _dbaitDbContext.Opinions.SingleOrDefaultAsync(o => o.Id == responseInput.OpinionId);
        if (opinion == null)
            throw new Exception("Opinion does not exist");

        var newResponse = new DbModels.Response
        {
            Id = 0,
            Content = responseInput.Content,
            Author = author,
            Opinion = opinion
        };

        var entry = _dbaitDbContext.Add(newResponse);
        await _dbaitDbContext.SaveChangesAsync();

        var entity = entry.Entity;
        return new GqlModels.Response
        {
            Id = entity.Id,
            Content = entity.Content,
            Author = new Author
            {
                Id = entity.Author.Id,
                Name = entity.Author.Name
            }
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _dbaitDbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
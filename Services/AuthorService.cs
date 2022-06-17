using GqlModels = DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using DbaitArgue.Queries.Inputs;
using DbaitArgue.Contexts;

namespace DbaitArgue.Services;

public class AuthorService : IAsyncDisposable
{
    private readonly DbaitDbContext _dbaitDbContext;

    public AuthorService(IDbContextFactory<DbaitDbContext> dbContextFactory)
    {
        _dbaitDbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<GqlModels.Author> Update(AuthorInput authorInput, int userId)
    {
        var authorEntity = await _dbaitDbContext.Authors.SingleOrDefaultAsync(a => a.Id == userId);
        if (authorEntity == null)
        {
            throw new Exception("Author does not exist");
        }

        authorEntity.Name = authorInput.Name;
        await _dbaitDbContext.SaveChangesAsync();

        return new GqlModels.Author
        {
            Id = authorEntity.Id,
            Name = authorEntity.Name
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _dbaitDbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
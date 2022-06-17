using GqlModels = DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using DbaitArgue.Queries.Inputs;
using DbaitArgue.Contexts;

namespace DbaitArgue.Services;

public class UserService : IAsyncDisposable
{
    private readonly DbaitDbContext _dbaitDbContext;

    public UserService(IDbContextFactory<DbaitDbContext> dbContextFactory)
    {
        _dbaitDbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<GqlModels.User> Create(UserInput userInput)
    {
        if (userInput.Password.Length < 10)
        {
            throw new Exception("Password must be atleast 10 characters");
        }

        var existingUser = _dbaitDbContext.Users.Any(u => u.Name == userInput.Name);
        if (existingUser)
        {
            throw new Exception("Username is already taken");
        }

        var newUser = new DbModels.User
        {
            Id = 0,
            Name = userInput.Name,
            Email = userInput.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userInput.Password),
            Author = new DbModels.Author
            {
                Id = 0,
                Name = userInput.Name
            }
        };

        var userEntry = _dbaitDbContext.Add(newUser);
        await _dbaitDbContext.SaveChangesAsync();

        var entity = userEntry.Entity;
        return new GqlModels.User
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Auhtor = new GqlModels.Author
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
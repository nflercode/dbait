using GqlModels = DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using DbaitArgue.Queries.Inputs;
using DbaitArgue.Contexts;
using BCrypt.Net;
using DbaitArgue.Utils;

namespace DbaitArgue.Services;

public class AuthService : IAsyncDisposable
{
    private readonly DbaitDbContext _dbaitDbContext;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(
        IDbContextFactory<DbaitDbContext> dbContextFactory,
        IJwtUtils jwtUtils)
    {
        _dbaitDbContext = dbContextFactory.CreateDbContext();
        _jwtUtils = jwtUtils;
    }

    public async Task<GqlModels.Auth> Authenticate(AuthInput authInput)
    {
        var user = await _dbaitDbContext.Users.SingleOrDefaultAsync(u => u.Name == authInput.Name);

        if (user == null || !BCrypt.Net.BCrypt.Verify(authInput.Password, user.Password))
        {
            throw new Exception("Username or password is invalid");
        }

        var authToken = _jwtUtils.GenerateToken(user.Id);
        return new GqlModels.Auth
        {
            AuthToken = authToken,
            User = new GqlModels.User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Auhtor = new GqlModels.Author
                {
                    Id = user.Author.Id,
                    Name = user.Author.Name
                }
            }
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _dbaitDbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
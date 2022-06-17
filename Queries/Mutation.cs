using DbaitArgue.Services;
using DbaitArgue.Queries.Payloads;
using DbaitArgue.Queries.Inputs;
using HotChocolate.AspNetCore.Authorization;
using DbaitArgue.Contexts;
using DbaitArgue.Queries.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DbaitArgue.Queries;

public class Mutation
{
    [Authorize]
    public async Task<AuthorPayload> UpdateAuthor(
        AuthorInput input,
        [Service] AuthorService authorService,
        ClaimsPrincipal claimsPrincipal)
    {
        var claimsUserId = claimsPrincipal.FindFirstValue("userId");
        var parsedUserId = int.Parse(claimsUserId);
        var updatedAuthor = await authorService.Update(input, parsedUserId);

        return new AuthorPayload
        {
            Author = updatedAuthor
        };
    }

    [Authorize]
    public async Task<OpinionPayload> CreateOpinion(
        OpinionInput input,
        [Service] OpinionService opinionService,
        ClaimsPrincipal claimsPrincipal)
    {
        var claimsUserId = claimsPrincipal.FindFirstValue("userId");
        var parsedUserId = int.Parse(claimsUserId);
        var newOpinion = await opinionService.Create(input, parsedUserId);
        return new OpinionPayload
        {
            Opinion = newOpinion
        };
    }

    [Authorize]
    public async Task<ResponsePayload> CreateResponse(
        ResponseInput input,
        [Service] ResponseService responseService,
        ClaimsPrincipal claimsPrincipal)
    {
        var claimsUserId = claimsPrincipal.FindFirstValue("userId");
        var parsedUserId = int.Parse(claimsUserId);
        var newResponse = await responseService.Create(input, parsedUserId);
        return new ResponsePayload
        {
            Response = newResponse
        };
    }

    public async Task<UserPayload> CreateUser(UserInput input, [Service] UserService userService)
    {
        var newUser = await userService.Create(input);
        return new UserPayload
        {
            User = newUser
        };
    }

    public async Task<AuthPayload> CreateToken(AuthInput input, [Service] AuthService authService)
    {
        var auth = await authService.Authenticate(input);
        return new AuthPayload
        {
            Auth = new Auth
            {
                AuthToken = auth.AuthToken,
                User = auth.User
            }
        };
    }
}
using BlazorLiveDemo.Server.DataAccess;
using BlazorLiveDemo.Server.DataAccess.Models;
using BlazorLiveDemo.Server.Services.Interfaces;
using BlazorLiveDemo.Shared.DTOs;

namespace BlazorLiveDemo.Server.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterHandler);
        app.MapPost("/user/login", LoginHandler);
        return app;
    }

    private static async Task<IResult> LoginHandler(IAuthService authService, UserLoginDto user)
    {
        var response = await authService.Login(user.Email, user.Password);
        return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> RegisterHandler(IAuthService authService, UserRegisterDto userDto)
    {
        var user = new UserModel() { Email = userDto.Email };
        var response = await authService.RegisterUserAsync(user, userDto.Password);
        return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    }

    public static WebApplication MapPersonEndpoints(this WebApplication app)
    {
        app.MapGet("/allPeople", async (IRepository<PersonDto> repo) =>
        {
            return Results.Ok(await repo.GetAllAsync());
        });

        app.MapPost("/addPerson", async (IRepository<PersonDto> repo, PersonDto person) =>
        {
            await repo.AddAsync(person);
            return Results.Ok("Added person");
        });
        return app;
    }
}
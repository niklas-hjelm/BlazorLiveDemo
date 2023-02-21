using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BlazorLiveDemo.Server.DataAccess;
using BlazorLiveDemo.Server.DataAccess.Models;
using BlazorLiveDemo.Server.Services.Interfaces;
using BlazorLiveDemo.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazorLiveDemo.Server.Services;

public class AuthService : IAuthService
{
    private readonly PeopleContext _peopleContext;
    private readonly IConfiguration _configuration;

    public AuthService(PeopleContext peopleContext, IConfiguration configuration)
    {
        _peopleContext = peopleContext;
        _configuration = configuration;
    }

    public async Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password)
    {
        if (await CheckUserExistsAsync(user.Email))
        {
            return new ServiceResponse<int>()
            {
                Success = false,
                Message = "User already exists!"
            };
        }

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _peopleContext.Users.AddAsync(user);
        await _peopleContext.SaveChangesAsync();

        return new ServiceResponse<int>()
        {
            Data = user.Id,
            Success = true,
            Message = "Registration Successful!"
        };
    }
    public async Task<bool> CheckUserExistsAsync(string email)
    {
        return await _peopleContext.Users
            .AnyAsync(user => user.Email
                .ToLower()
                .Equals(email.ToLower()));
    }
    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();

        if (!await CheckUserExistsAsync(email))
        {
            response.Success = false;
            response.Message = "Wrong user name or password!";
            return response;
        }
        var user = await _peopleContext.Users.FirstAsync(u => u.Email == email);

        if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong user name or password!";
            return response;
        }

        response.Success = true;
        response.Message = "TJOHOOOOO!";

        response.Data = CreateToken(user);

        return response;
    }

    private string CreateToken(UserModel user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Email)
        };

        var secret = _configuration.GetSection("AppSettings:Token").Value;

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        using (var hmac = new HMACSHA512(userPasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(userPasswordHash);
        }
    }

    private void CreatePasswordHash(
        string password,
        out byte[] passwordHash,
        out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

}
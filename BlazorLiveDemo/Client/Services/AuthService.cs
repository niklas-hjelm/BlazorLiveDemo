using System.Net.Http.Json;
using BlazorLiveDemo.Client.Services.Interfaces;
using BlazorLiveDemo.Shared;
using BlazorLiveDemo.Shared.DTOs;

namespace BlazorLiveDemo.Client.Services;

public class AuthService :IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ServiceResponse<int>?> RegisterAsync(UserRegisterDto userDto)
    {
        var response = await _httpClient.PostAsJsonAsync("user/register",userDto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>?> LoginAsync(UserLoginDto userDto)
    {
        var response = await _httpClient.PostAsJsonAsync("user/login", userDto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }
}
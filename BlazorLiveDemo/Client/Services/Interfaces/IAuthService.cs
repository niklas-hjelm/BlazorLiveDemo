using BlazorLiveDemo.Shared;
using BlazorLiveDemo.Shared.DTOs;

namespace BlazorLiveDemo.Client.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<int>?> RegisterAsync(UserRegisterDto userDto);
    Task<ServiceResponse<string>?> LoginAsync(UserLoginDto userDto);
}
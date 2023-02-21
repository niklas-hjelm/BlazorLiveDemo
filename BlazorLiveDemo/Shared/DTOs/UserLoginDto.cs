using System.ComponentModel.DataAnnotations;

namespace BlazorLiveDemo.Shared.DTOs;

public class UserLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password can not be empty!")]
    public string Password { get; set; }
}
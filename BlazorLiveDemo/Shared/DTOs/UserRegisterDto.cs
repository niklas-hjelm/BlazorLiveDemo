using System.ComponentModel.DataAnnotations;

namespace BlazorLiveDemo.Shared.DTOs;

public class UserRegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
}
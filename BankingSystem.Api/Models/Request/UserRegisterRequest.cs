using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Api.Models.Request;

public class UserRegisterRequest
{
    /// <summary>
    /// Nmae of the user
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Email Address of the user
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password 
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Confirm Password
    /// </summary>
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;
}

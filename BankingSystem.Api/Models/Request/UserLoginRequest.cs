using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Api.Models.Request;

public class UserLoginRequest
{
    /// <summary>
    /// User Id 
    /// </summary>
    [Required]
    public int CustomerId { get; set; }
    /// <summary>
    /// Password
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}

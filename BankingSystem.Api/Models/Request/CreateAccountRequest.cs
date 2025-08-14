using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Api.Models.Request;

public class CreateAccountRequest
{
    /// <summary>
    /// Type of Account : Saving, Checking
    /// </summary>
    [Required]
    public string AccountType { get; set; } = null!;

    /// <summary>
    /// User Id on which account is going to create
    /// </summary>
    [Required]
    public int UserId { get; set; }
}

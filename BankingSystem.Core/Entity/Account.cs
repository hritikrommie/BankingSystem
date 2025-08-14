using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Core.Entity;

public class Account
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    [Required]
    public string AccountType { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public byte[]? RowVersion { get; set; } // ForConcurrency

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

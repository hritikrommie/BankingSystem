using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Core.Entity;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public Guid? ReferenceId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = string.Empty; // Deposit, Withdraw, TransferIn, TransferOut
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }

    public Account Account { get; set; }
}
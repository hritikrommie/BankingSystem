using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Core.Dtos;

public class TransactionDtos
{
    public int Id { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public Guid? ReferenceId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = string.Empty; // Deposit, Withdraw, TransferIn, TransferOut
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
    public decimal NewBalance { get; set; }
}

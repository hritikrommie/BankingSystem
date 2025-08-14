namespace BankingSystem.Core.Dtos;

public class AccountDtos
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}

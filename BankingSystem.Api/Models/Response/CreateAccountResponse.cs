namespace BankingSystem.Api.Models.Response;

public class CreateAccountResponse
{
    /// <summary>
    /// Account Id, this will be used for future transactions
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Account Number
    /// </summary>
    public string AccountNumber { get; set; } = null!;

    /// <summary>
    /// Current balance of the account
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Type of account
    /// </summary>
    public string AccountType { get; set; } = null!;

    /// <summary>
    /// Date and time when account created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

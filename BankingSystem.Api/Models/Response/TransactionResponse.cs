namespace BankingSystem.Api.Models.Response;

public class TransactionResponse
{
    /// <summary>
    /// Transaction message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// Transaction Id 
    /// </summary>
    public int TransactionId { get; set; }
    /// <summary>
    /// New Balance of the account after transaction
    /// </summary>
    public decimal NewBalance { get; set; }
}

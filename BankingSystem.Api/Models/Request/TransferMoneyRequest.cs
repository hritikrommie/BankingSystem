namespace BankingSystem.Api.Models.Request;

public class TransferMoneyRequest
{
    /// <summary>
    /// Account Id from which the amount is getting transferred.
    /// In case of Deposit, It will be null.
    /// </summary>
    public int FromAccountId { get; set; }

    /// <summary>
    /// Account Id to which the amount is getting transferred.
    /// In case of Withdraw, It will be null.
    /// </summary>
    public int ToAccountId { get; set; }

    /// <summary>
    /// Transaction Amount
    /// </summary>
    public decimal Amount { get; set; }
}

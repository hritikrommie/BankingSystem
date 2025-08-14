namespace BankingSystem.Api.Models.Request;

public class WithdrawMoneyRequest
{
    /// <summary>
    /// Account from which amount will be withdrawn
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Transaction Amount
    /// </summary>
    public decimal Amount { get; set; }
}

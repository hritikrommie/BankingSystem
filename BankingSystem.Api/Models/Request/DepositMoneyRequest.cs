using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Api.Models.Request;

public class DepositMoneyRequest
{
    /// <summary>
    /// Account in which amout will be deposited.
    /// </summary>
    [Required]
    public int AccountId { get; set; }

    /// <summary>
    /// Transaction Amount
    /// </summary>
    [Required]
    public decimal Amount { get; set; }
}

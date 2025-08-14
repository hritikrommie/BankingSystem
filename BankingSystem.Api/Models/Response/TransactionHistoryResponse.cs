using BankingSystem.Core.Entity;

namespace BankingSystem.Api.Models.Response;

public class TransactionHistoryResponse
{
    /// <summary>
    /// Tran Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Reference Id , It will be populated in case of transfer monehy, so can identify in/out transactions
    /// </summary>
    public Guid? ReferenceId { get; set; }
    /// <summary>
    /// Account Id
    /// </summary>
    public int AccountId { get; set; }
    /// <summary>
    /// Transaction Amount
    /// </summary>
    public decimal Amount { get; set; }
    /// <summary>
    /// Type of Transaction
    /// </summary>
    public string TransactionType { get; set; } = string.Empty;
    /// <summary>
    /// Date of Transaction
    /// </summary>
    public DateTime TransactionDate { get; set; }
    /// <summary>
    /// Transaction Description
    /// </summary>
    public string? Description { get; set; }
}

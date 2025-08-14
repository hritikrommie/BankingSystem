using BankingSystem.Core.Dtos;

namespace BankingSystem.Core.Interfaces;

public interface ITransactionService
{
    Task<TransactionDtos> Deposit(TransactionDtos transaction, CancellationToken cancellationToken = default);
    Task<TransactionDtos> Withdraw(TransactionDtos transaction, CancellationToken cancellationToken= default);
    Task<TransactionDtos> Transfer(TransactionDtos transaction, CancellationToken cancellationToken = default);
    Task<List<TransactionDtos>> GetAllTransactions(CancellationToken cancellationToken = default);
    Task<List<TransactionDtos>> GetTransactionByAccountId(int accountId, CancellationToken cancellationToken = default);
}

using BankingSystem.Core.Entity;
using Microsoft.Extensions.ObjectPool;

namespace BankingSystem.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    IRepository<Account> Accounts { get; }
    IRepository<Transaction> Transactions { get; }

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
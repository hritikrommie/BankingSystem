using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using System;

namespace BankingSystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _context;
    public IRepository<User> Users { get; }
    public IRepository<Account> Accounts { get; }
    public IRepository<Transaction> Transactions { get; }

    public UnitOfWork(AppDBContext context, IRepository<User> users, IRepository<Account> accounts, IRepository<Transaction> transactions)
    {
        _context = context;
        Users = users;
        Accounts = accounts;
        Transactions = transactions;
    }

    public Task BeginTransactionAsync() => _context.Database.BeginTransactionAsync();
    public Task CommitAsync() => _context.Database.CommitTransactionAsync();
    public Task RollbackAsync() => _context.Database.RollbackTransactionAsync();
}
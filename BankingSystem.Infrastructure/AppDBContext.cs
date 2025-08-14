using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using BankingSystem.Core.Entity;

namespace BankingSystem.Infrastructure;

public class AppDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dateData = new DateTime(2025, 8, 13);

        modelBuilder.Entity<Account>()
        .Property(a => a.RowVersion)
        .IsRowVersion();

        #region Data Seed Only for Testing 
        modelBuilder.Entity<User>().HasData(
     new User { Id = 1, Name = "John Doe", CreatedAt = dateData, Email = "abc@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 2, Name = "Alice Smith", CreatedAt = dateData, Email = "alice@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 3, Name = "Bob Johnson", CreatedAt = dateData, Email = "bob@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 4, Name = "Charlie Brown", CreatedAt = dateData, Email = "charlie@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 5, Name = "David Lee", CreatedAt = dateData, Email = "david@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 6, Name = "Emma Davis", CreatedAt = dateData, Email = "emma@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 7, Name = "Frank Wilson", CreatedAt = dateData, Email = "frank@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 8, Name = "Grace Taylor", CreatedAt = dateData, Email = "grace@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 9, Name = "Henry Clark", CreatedAt = dateData, Email = "henry@gmail.com", LastUpdatedAt = dateData, Role = "Customer", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" },
     new User { Id = 10, Name = "Admin", CreatedAt = dateData, Email = "admin@gmail.com", LastUpdatedAt = dateData, Role = "Admin", Password = "AQAAAAIAAYagAAAAEM96MDF+PEBOHoLVChJ4UUQUtTLD7Ol6kyhC7gAmdkF7lhwCHL+SHBpXGTl4WVNCJA==" }
 );

        modelBuilder.Entity<Account>().HasData(
            new Account() { Id = 1, AccountNumber = "12345", AccountType = "Saving", Balance = 200, CreatedAt = dateData, UserId = 1 },
            new Account() { Id = 2, AccountNumber = "12345", AccountType = "Checking", Balance = 1000, CreatedAt = dateData, UserId = 1 },
            new Account() { Id = 3, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 2 },
            new Account() { Id = 4, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 3 },
            new Account() { Id = 5, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 4 },
            new Account() { Id = 6, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 5 },
            new Account() { Id = 7, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 6 },
            new Account() { Id = 8, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 7 },
            new Account() { Id = 9, AccountNumber = "12345", AccountType = "Saving", Balance = 100, CreatedAt = dateData, UserId = 8 }
            );

        modelBuilder.Entity<Transaction>().HasData(
            new Transaction() { Id = 1, AccountId = 1, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited"},
            new Transaction() { Id = 2, AccountId = 2, Amount = 1000, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 3, AccountId = 3, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 4, AccountId = 4, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 5, AccountId = 5, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 6, AccountId = 6, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 7, AccountId = 7, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 8, AccountId = 8, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 9, AccountId = 9, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" },
            new Transaction() { Id = 10, AccountId = 1, Amount = 100, TransactionDate = dateData, TransactionType = "Deposit", Description = "Money Deposited" }
            );

        #endregion
    }
}

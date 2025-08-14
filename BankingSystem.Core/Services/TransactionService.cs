using AutoMapper;
using BankingSystem.Core.Common;
using BankingSystem.Core.Dtos;
using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Core.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionService> _logger;
    private readonly IMapper _mapper;
    public TransactionService(IUnitOfWork unitOfWork, ILogger<TransactionService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDtos> Deposit(TransactionDtos transaction, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Deposit)} starts executing.");
        TransactionDtos response;
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var account = await _unitOfWork.Accounts.GetByIdAsync(transaction.AccountId);
            if (account == null)
                throw new AppException("Please provide correct account details.");

            account.Balance += transaction.Amount;
            var newTransaction = _mapper.Map<Transaction>(transaction);
            newTransaction.TransactionDate = DateTime.Now;
            newTransaction.TransactionType = "Deposit";
            newTransaction.Description = "Money Deposited";
            await _unitOfWork.Transactions.AddAsync(newTransaction);
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.Accounts.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            response = _mapper.Map<TransactionDtos>(newTransaction);
            response.NewBalance = account.Balance;
        }
        catch (DbUpdateConcurrencyException)
        {
            await _unitOfWork.RollbackAsync();
            // Add this to job table, will be processed by worker
            throw new AppException("We are processing your transaction.");
        }
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Deposit)} stops executing.");
        return response;
    }

    public async Task<List<TransactionDtos>> GetAllTransactions(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(GetAllTransactions)} starts executing.");
        var allTransactions = await _unitOfWork.Transactions.GetAllAsync();
        var response = _mapper.Map<List<TransactionDtos>>(allTransactions.ToList());
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(GetAllTransactions)} stops executing.");
        return response;
    }

    public async Task<List<TransactionDtos>> GetTransactionByAccountId(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(GetTransactionByAccountId)} starts executing.");
        var allTransactions = await _unitOfWork.Transactions.GetAllAsync();
        var response = _mapper.Map<List<TransactionDtos>>(allTransactions.Where(x => x.AccountId == accountId));
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(GetTransactionByAccountId)} stops executing.");
        return response;
    }

    public async Task<TransactionDtos> Transfer(TransactionDtos transaction, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Transfer)} starts executing.");
        TransactionDtos response;
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var fromAccount = await _unitOfWork.Accounts.GetByIdAsync(transaction.FromAccountId);
            var toAccount = await _unitOfWork.Accounts.GetByIdAsync(transaction.ToAccountId);
            if (fromAccount == null || toAccount == null)
                throw new AppException("Please provide correct account details.");
            if (fromAccount.Balance < transaction.Amount)
                throw new AppException("Insufficient balance in account.");

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;
            var referenceid = Guid.NewGuid();
            var fromAccountTransaction = new Transaction()
            {
                AccountId = transaction.FromAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = "TransferOut",
                Description = "Money Transfered",
                Amount = transaction.Amount,
                ReferenceId = referenceid
            };
            var toAccountTransaction = new Transaction()
            {
                AccountId = transaction.ToAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = "TransferIn",
                Description = "Money Received",
                Amount = transaction.Amount,
                ReferenceId = referenceid
            };

            await _unitOfWork.Transactions.AddAsync(fromAccountTransaction);
            await _unitOfWork.Transactions.AddAsync(toAccountTransaction);
            _unitOfWork.Accounts.Update(fromAccount);
            _unitOfWork.Accounts.Update(toAccount);
            await _unitOfWork.Accounts.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            response = _mapper.Map<TransactionDtos>(fromAccountTransaction);
            response.AccountId = transaction.FromAccountId;
            response.NewBalance = fromAccount.Balance;
        }
        catch (DbUpdateConcurrencyException)
        {
            await _unitOfWork.RollbackAsync();
            // Add this to job table, will be processed by worker
            throw new AppException("We are processing your transaction ");
        }
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Transfer)} stops executing.");
        return response;
    }

    public async Task<TransactionDtos> Withdraw(TransactionDtos transaction, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Withdraw)} starts executing.");
        TransactionDtos response;
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var account = await _unitOfWork.Accounts.GetByIdAsync(transaction.AccountId);
            if (account == null)
                throw new AppException("Please provide correct account details.");
            if (account.Balance < transaction.Amount)
                throw new AppException("Insufficient balance in account.");

            account.Balance -= transaction.Amount;
            var newTransaction = _mapper.Map<Transaction>(transaction);
            newTransaction.TransactionDate = DateTime.Now;
            newTransaction.TransactionType = "Withdraw";
            newTransaction.Description = "Money Withdrawn";
            await _unitOfWork.Transactions.AddAsync(newTransaction);
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.Accounts.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            response = _mapper.Map<TransactionDtos>(newTransaction);
            response.NewBalance = account.Balance;
        }
        catch (DbUpdateConcurrencyException)
        {
            await _unitOfWork.RollbackAsync();
            // Add this to job table, will be processed by worker
            throw new AppException("We are processing your transaction ");
        }
        _logger.LogInformation($"Class : {nameof(TransactionService)} Method : {nameof(Withdraw)} stops executing.");
        return response;
    }
}

using AutoMapper;
using BankingSystem.Core.Dtos;
using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace BankingSystem.Core.Services;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AccountService(ILogger<AccountService> logger,IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<AccountDtos> CreateAccount(AccountDtos account, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(CreateAccount)} starts executing.");
        var addAccount = _mapper.Map<Account>(account);
        addAccount.CreatedAt = DateTime.Now;
        await _unitOfWork.Accounts.AddAsync(addAccount);
        await _unitOfWork.Accounts.SaveChangesAsync();
        var response = _mapper.Map<AccountDtos>(addAccount);
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(CreateAccount)} stops executing.");
        return response;
    }

    public async Task<AccountDtos> GetAccount(int accountId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccount)} starts executing.");
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);
        var response = _mapper.Map<AccountDtos>(account);
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccount)} stops executing.");
        return response;
    }

    public async Task<List<AccountDtos>> GetAccounts(int userid, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccounts)} starts executing.");
        var accounts = await _unitOfWork.Accounts.GetAllAsync();
        var response = _mapper.Map<List<AccountDtos>>(accounts.Where(x => x.UserId == userid));
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccounts)} stops executing.");
        return response;
    }

    public async Task<AccountDtos> UpdateAccount(AccountDtos accountDtos, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccount)} starts executing.");
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountDtos.Id);
        account!.AccountType = accountDtos.AccountType;
        account.Balance = accountDtos.Balance;
        await _unitOfWork.Accounts.SaveChangesAsync();
        var response = _mapper.Map<AccountDtos>(account);
        _logger.LogInformation($"Class : {nameof(AccountService)} Method : {nameof(GetAccount)} stops executing.");
        return response;
    }
}

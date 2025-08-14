using BankingSystem.Core.Dtos;

namespace BankingSystem.Core.Interfaces;

public interface IAccountService
{
    Task<AccountDtos> CreateAccount(AccountDtos account, CancellationToken cancellationToken = default);
    Task<AccountDtos> GetAccount(int accountId, CancellationToken cancellationToken = default);
    Task<AccountDtos> UpdateAccount(AccountDtos accountDtos, CancellationToken cancellationToken = default);
    Task<List<AccountDtos>> GetAccounts(int userid, CancellationToken cancellationToken = default);
}

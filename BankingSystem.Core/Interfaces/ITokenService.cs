using BankingSystem.Core.Dtos;

namespace BankingSystem.Core.Interfaces;

public interface ITokenService
{
    Task<TokenDtos> GetToken(UserDtos user);
}

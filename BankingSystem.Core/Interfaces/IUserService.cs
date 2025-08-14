using BankingSystem.Core.Dtos;

namespace BankingSystem.Core.Interfaces;

public interface IUserService
{
    Task<bool> ValidatePassword(UserDtos user, string password, CancellationToken cancellationToken = default);
    Task<UserDtos?> GetUser(int id, CancellationToken cancellationToken = default);
    Task<List<UserDtos>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<UserDtos> CreateUser(UserDtos user, CancellationToken cancellationToken = default);
}

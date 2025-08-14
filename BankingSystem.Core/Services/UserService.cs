using AutoMapper;
using BankingSystem.Core.Common;
using BankingSystem.Core.Dtos;
using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Runtime.Intrinsics.X86;

namespace BankingSystem.Core.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserDtos> CreateUser(UserDtos user, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(CreateUser)} starts executing.");

        var addUser = _mapper.Map<User>(user);
        addUser.Password = Crypto.HashPassword(user.Password!);
        addUser.CreatedAt = DateTime.Now;
        addUser.LastUpdatedAt = DateTime.Now;
        addUser.Role = "Customer";
        await _unitOfWork.Users.AddAsync(addUser);
        await _unitOfWork.Users.SaveChangesAsync();

        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(CreateUser)} stops executing.");
        return _mapper.Map<UserDtos>(addUser);
    }

    public async Task<List<UserDtos>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(GetUser)} starts executing.");
        var users = await _unitOfWork.Users.GetAllAsync();
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(GetUser)} stops executing.");
        return _mapper.Map<List<UserDtos>>(users.ToList());
    }

    public async Task<UserDtos?> GetUser(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(GetUser)} starts executing.");
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(GetUser)} stops executing.");
        return _mapper.Map<UserDtos>(user);
    }

    public Task<bool> ValidatePassword(UserDtos user, string password, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(ValidatePassword)} starts executing.");
        var response = Crypto.VerifyPassword(user.Password!, password);
        _logger.LogInformation($"Class : {nameof(UserService)} Method : {nameof(ValidatePassword)} stops executing.");
        return Task.FromResult(response);
    }
}

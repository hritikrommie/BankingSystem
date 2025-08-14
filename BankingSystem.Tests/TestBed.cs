using AutoMapper;
using BankingSystem.Api.Controllers;
using BankingSystem.Api.Profiles;
using BankingSystem.Core.Interfaces;
using BankingSystem.Core.Services;
using BankingSystem.Infrastructure;
using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankingSystem.Tests;

public class TestBed : IDisposable
{
    public AuthController AuthController { get; set; }
    public AccountController AccountController { get; set; }
    public TransactionController TransactionController { get; set; }
    private IMapper Mapper { get; set; }
    private IServiceProvider ServiceProvider { get; set; }
    public Mock<IUnitOfWork> _unitofWork { get; set; }
    public Mock<ITokenService> _tokenService { get; set; }
    public TestBed()
    {
        var services = new ServiceCollection();
        _unitofWork = new Mock<IUnitOfWork>();
        _tokenService = new Mock<ITokenService>();

        services.AddAutoMapper(typeof(MapperProfile)); // register all profiles if needed
        services.AddAutoMapper(typeof(BankingSystem.Core.MapperProfile));

        services.AddSingleton<IUnitOfWork>(_unitofWork.Object);
        services.AddSingleton<ITokenService>(_tokenService.Object);
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        services.AddSingleton(loggerFactory);
        services.AddScoped(typeof(ILogger<>), typeof(Logger<>));

        ServiceProvider = services.BuildServiceProvider();

        Mapper = ServiceProvider.GetRequiredService<IMapper>();

        AuthController = new AuthController(ServiceProvider.GetRequiredService<IUserService>(), ServiceProvider.GetRequiredService<ITokenService>(), ServiceProvider.GetRequiredService<ILogger<AuthController>>(), ServiceProvider.GetRequiredService<IMapper>());
    }

    public void Dispose()
    {
    }
}
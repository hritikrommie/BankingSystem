using AutoMapper;
using BankingSystem.Core.Common;
using BankingSystem.Core.Dtos;
using BankingSystem.Core.Entity;
using BankingSystem.Core.Interfaces;
using BankingSystem.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.RateLimiting;

namespace BankingSystem.Core;

public static class RegisterCore
{
    public static IServiceCollection RegisterCoreServicesForWorker(this IServiceCollection services)
    {
        services.RegisterDependencuies();
        return services;
    }
    public static IServiceCollection RegisterCoreServicesForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtTokenService(configuration);
        services.RegisterDependencuies();
        services.AddRateLimiting();
        services.AddResponseCaching();
        return services;
    }
    public static IApplicationBuilder RegisterCoreAppForWorker(this IApplicationBuilder app)
    {
        return app;
    }
    public static IApplicationBuilder RegisterCoreAppForApi(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExecutionMiddleware>();
        app.UseRateLimiter();
        app.UseResponseCaching();
        return app;
    }
    private static IServiceCollection AddJwtTokenService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var secretKey = configuration["Jwt:Key"] ?? "This is a secret key used to generate token in Jwt.";
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];

                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        services.AddAuthorization();
        return services;
    }
    private static IServiceCollection RegisterDependencuies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
    private static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,           
                        Window = TimeSpan.FromSeconds(10), 
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });
        return services;
    }
}

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDtos, User>().ReverseMap();
        CreateMap<TransactionDtos, Transaction>().ReverseMap();
        CreateMap<AccountDtos, Account>().ReverseMap();

    }
}

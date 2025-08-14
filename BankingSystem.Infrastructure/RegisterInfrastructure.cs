using BankingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BankingSystem.Infrastructure;

public static class RegisterInfrastructure
{
    public static IServiceCollection RegisterInfraStructureServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDBContext>(options =>
                            options.UseInMemoryDatabase("TestDb")
                            .ConfigureWarnings(x =>
                        x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static WebApplication RegisterInfraStructureApp(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            db.Database.EnsureCreated();
        }
        return app;
    }
}

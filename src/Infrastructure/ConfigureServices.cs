using System;
using EmployeePayroll.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "_developmentSpecificOrigins",
                                policy =>
                                {
                                    policy.WithOrigins("http://localhost:3000").AllowAnyHeader();
                                });
        });

        services.AddDbContext<ApplicationDbContext>(option => option.UseInMemoryDatabase("EmployeePayroll"));

        return services;
    }
}
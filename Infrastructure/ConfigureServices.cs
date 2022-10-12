using System;
using EmployeePayroll.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "_developmentSpecificOrigins",
                                policy =>
                                {
                                    policy.WithOrigins("http://localhost:3000").AllowAnyHeader();
                                });
        });

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateConverter());
                options.JsonSerializerOptions.Converters.Add(new CurrencyConverter());
            });

        services.AddDbContext<TimeReportContext>(option => option.UseInMemoryDatabase("EmployeePayroll"));

        return services;
    }
}
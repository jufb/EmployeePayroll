using System;
using EmployeePayroll.Infrastructure.Converters;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateConverter());
                options.JsonSerializerOptions.Converters.Add(new CurrencyConverter());
            });

        return services;
    }
}
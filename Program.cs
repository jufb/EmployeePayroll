using EmployeePayroll.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateConverter());
        options.JsonSerializerOptions.Converters.Add(new CurrencyConverter());
    });

builder.Services.AddDbContext<TimeReportContext>(option => option.UseInMemoryDatabase("EmployeePayroll"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
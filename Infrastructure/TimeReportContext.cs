using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Infrastructure;

public class TimeReportContext : DbContext
{
    public TimeReportContext(DbContextOptions<TimeReportContext> options)
        : base(options)
    {
    }

    public DbSet<TimeReport> TimeReports { get; set; } = null!;
}
using System;
using EmployeePayroll.Infrastructure.Files;
using EmployeePayroll.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll.Application.TimeReports;

public class CreateTimeReportCommand
{
    private readonly ApplicationDbContext _context;

    public CreateTimeReportCommand(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateTimeReport(Stream file, long reportId)
    {
        try
        {
            foreach (var timeReport in CsvFileReader.LoadCsv(file))
            {
                timeReport.ReportId = reportId;

                _context.TimeReports.Add(timeReport);
            }

            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }
}
using System;
using EmployeePayroll.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll.Application;

public class CreateTimeReportCommand
{
    private readonly TimeReportContext _context;

    public CreateTimeReportCommand(TimeReportContext context)
    {
        _context = context;
    }

    public async Task<int> CreateTimeReport(string file, long reportId)
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
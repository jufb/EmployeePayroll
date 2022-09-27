using System;
using EmployeePayroll.Application;
using EmployeePayroll.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayroll.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeReportController : ControllerBase
{
    private readonly TimeReportContext _context;

    public TimeReportController(TimeReportContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<PayrollReportDTO>> GetPayrollReport()
    {
        if (_context.TimeReports == null) return Problem("Error: Entity set 'TimeReportContext.TimeReports' is null.");

        return await new GetPayrollReportQuery(_context).GetQuery();
    }

    [HttpPost]
    public async Task<ActionResult<PayrollReportDTO>> Create(string file)
    {
        if (_context.TimeReports == null) return Problem("Error: Entity set 'TimeReportContext.TimeReports' is null.");

        try
        {
            (bool Exist, long ReportId) result = new GetTimeReportsQuery(_context).ReportIdExists(file);

            if (result.Exist) return Problem("Error: Time Report Id already exists. Please try again with a new file.");
            
            await new CreateTimeReportCommand(_context).CreateTimeReport(file, result.ReportId);

            return await GetPayrollReport();
        }
        catch
        {
            return Problem("Error: Could not include the file.");
        }
    }
}
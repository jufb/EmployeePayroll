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
    public async Task<ActionResult<IEnumerable<TimeReportDTO>>> GetTimeReport()
    {
        if (_context.TimeReports == null)
        {
            return NotFound();
        }

        return await new GetTimeReportsQuery(_context).GetQuery();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeReportDTO>> GetTimeReport(long id)
    {
        if (_context.TimeReports == null)
        {
            return NotFound();
        }

        TimeReportDTO? timeReportDto = await new GetTimeReportsQuery(_context).GetQuery(id);

        if (timeReportDto == null)
        {
            return NotFound();
        }

        return timeReportDto;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<TimeReportDTO>>> Create(string file)
    {
        if (_context.TimeReports == null) return Problem("Entity set 'TimeReportContext.TimeReports' is null.");

        try
        {
            (bool Exist, long ReportId) result = new GetTimeReportsQuery(_context).ReportIdExists(file);
            if (result.Exist)
            {
                return Problem("Time Report Id already exists. Please try again with a new file.");
            }
            
            await new CreateTimeReportCommand(_context).CreateTimeReport(file, result.ReportId);

            //Remove after test purposes
            return await GetTimeReport();

            //return await new GetPayrollReportQuery(_context).GetQuery();
        }
        catch
        {
            return Problem("Error: Could not include the file.");
        }
    }
}
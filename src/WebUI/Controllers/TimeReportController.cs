using EmployeePayroll.Application.Models;
using EmployeePayroll.Application.PayrollReports;
using EmployeePayroll.Application.TimeReports;
using EmployeePayroll.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayroll.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeReportController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TimeReportController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<PayrollReportDTO>> GetPayrollReport()
    {
        if (_context.TimeReports == null)
            return Problem("Error: Entity set 'ApplicationDbContext.TimeReports' is null.");

        return await new GetPayrollReportQuery(_context).GetQuery();
    }

    [HttpPost]
    public async Task<ActionResult<PayrollReportDTO>> CreateFile()
    {
        if (_context.TimeReports == null)
            return Problem("Error: Entity set 'ApplicationDbContext.TimeReports' is null.");

        if (HttpContext.Request.ContentLength == 0)
        {
            return BadRequest("Bad Request: File was not included.");
        }

        var formFile = HttpContext.Request.Form.Files[0];

        (bool Exist, long ReportId) result = new GetTimeReportsQuery(_context).ReportIdExists(formFile.FileName);

        if (result.ReportId.Equals(0))
            return BadRequest("Bad Request: File format is not accepted.");

        if (result.Exist)
            return BadRequest("Bad Request: Time Report Id already exists. Please try again with a new file.");

        try
        {
            if (new CreateTimeReportCommand(_context).CreateTimeReport(formFile.OpenReadStream(), result.ReportId).Result.Equals(0))
                return NoContent();

            return await GetPayrollReport();
        }
        catch (Exception e)
        {
            return Problem("Error: Could not include the file. Exception description: " + e.ToString());
        }
    }
}
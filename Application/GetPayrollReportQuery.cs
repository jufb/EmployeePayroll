using System;
using EmployeePayroll.Domain.Entities;
using EmployeePayroll.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll.Application;

public class GetPayrollReportQuery
{
    private readonly TimeReportContext _context;

    public GetPayrollReportQuery(TimeReportContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<TimeReportDTO>>> GetQuery()
    {
        return await _context.TimeReports
                    .Select(t => ItemToDTO(t))
                    .ToListAsync();
    }

    public async Task<PayrollReport?> GetQuery(long id)
    {
        IEnumerable<TimeReportDTO> timeReports = await _context.TimeReports
                    .Select(t => ItemToDTO(t))
                    .ToListAsync();

        PayrollReport payrollReport = new PayrollReport();

        return null;
    }


    private static TimeReport DTOToItem(TimeReport? timeReport, TimeReportDTO timeReportDTO)
    {
        if (timeReport == null)
        {
            timeReport = new TimeReport();
        }

        timeReport.Date = timeReportDTO.Date;
        timeReport.EmployeeId = timeReportDTO.EmployeeId;
        timeReport.HoursWorked = timeReportDTO.HoursWorked;
        timeReport.JobGroup = timeReportDTO.JobGroup;
        timeReport.ReportId = timeReportDTO.ReportId;

        return timeReport;
    }

    private static TimeReportDTO ItemToDTO(TimeReport timeReport) =>
        new TimeReportDTO
        {
            Id = timeReport.Id,
            Date = timeReport.Date,
            EmployeeId = timeReport.EmployeeId,
            HoursWorked = timeReport.HoursWorked,
            JobGroup = timeReport.JobGroup,
            ReportId = timeReport.ReportId
        };
}
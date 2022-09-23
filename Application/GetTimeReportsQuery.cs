using System;
using System.Text.RegularExpressions;
using EmployeePayroll.Domain.Entities;
using EmployeePayroll.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll.Application;

public class GetTimeReportsQuery
{
    private readonly TimeReportContext _context;

    public GetTimeReportsQuery(TimeReportContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<TimeReportDTO>>> GetQuery()
    {
        return await _context.TimeReports
                    .Select(t => ItemToDTO(t))
                    .ToListAsync();
    }

    public async Task<TimeReportDTO?> GetQuery(long id)
    {
        TimeReport? timeReport =  await _context.TimeReports.FindAsync(id);

        if(timeReport is not null)
        {
            return ItemToDTO(timeReport);
        }

        return null;
    }

    public (bool,long) ReportIdExists(string file)
    {
        long reportId = GetNumbersFromString(file);

        if (GetTimeReportIdQuery(reportId))
        {
            return (true,reportId);
        }

        return (false,reportId);
    }

    private static long GetNumbersFromString(string file)
    {
        string[] numbers = Regex.Matches(file, @"(?<=-)\d+?(?=.csv)")
            .Cast<Match>()
            .Select(n => n.Value)
            .ToArray();
        
        return long.Parse(String.Join(",", numbers));
    }

    private bool GetTimeReportQuery(long id)
    {
        return (_context.TimeReports?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private bool GetTimeReportIdQuery(long id)
    {
        return (_context.TimeReports?.Any(e => e.ReportId == id)).GetValueOrDefault();
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
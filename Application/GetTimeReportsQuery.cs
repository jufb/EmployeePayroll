﻿using System;
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

    public async Task<IEnumerable<TimeReportDTO>> GetQuery()
    {
        return await _context.TimeReports
                    .OrderBy(t => t.EmployeeId)
                    .ThenBy(t => t.Date)
                    .Select(t => ItemToDTO(t))
                    .ToListAsync();
    }

    public (bool,long) ReportIdExists(string file)
    {
        long reportId = GetNumbersFromString(file);

        if ((_context.TimeReports?.Any(e => e.ReportId == reportId)).GetValueOrDefault())
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
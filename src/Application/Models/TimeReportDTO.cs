using System;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Application.Models;

public class TimeReportDTO
{
    public long Id { get; }
    public long ReportId { get; }
    public DateTime Date { get; }
    public float HoursWorked { get; }
    public long EmployeeId { get; }
    public char? JobGroup { get; }

    public TimeReportDTO(TimeReport timeReport)
    {
        Id = timeReport.Id;
        Date = timeReport.Date;
        HoursWorked = timeReport.HoursWorked;
        EmployeeId = timeReport.EmployeeId;
        JobGroup = timeReport.JobGroup;
        ReportId = timeReport.ReportId;
    }
}
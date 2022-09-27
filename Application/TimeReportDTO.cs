using System;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Application;

public class TimeReportDTO
{
    public long Id { get; set; }
    public long ReportId { get; set; }
    public DateTime Date { get; set; }
    public float HoursWorked { get; set; }
    public long EmployeeId { get; set; }
    public char? JobGroup { get; set; }

    public TimeReportDTO() { }
    public TimeReportDTO(TimeReport timeReport) =>
    (
        Id,
        Date,
        HoursWorked,
        EmployeeId,
        JobGroup,
        ReportId
    ) = (
        timeReport.Id,
        timeReport.Date,
        timeReport.HoursWorked,
        timeReport.EmployeeId,
        timeReport.JobGroup,
        timeReport.ReportId
    );
}
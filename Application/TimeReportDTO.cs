using System;
namespace EmployeePayroll.Application;

public class TimeReportDTO
{
    public long Id { get; set; }
    public long ReportId { get; set; }
    public DateTime Date { get; set; }
    public float HoursWorked { get; set; }
    public long EmployeeId { get; set; }
    public char? JobGroup { get; set; }
}
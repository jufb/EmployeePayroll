using System;
namespace EmployeePayroll.Domain.Entities;

public class TimeReport
{
    public long Id { get; set; }
    public long ReportId { get; set; }
    public DateTime Date { get; set; }
    public float HoursWorked { get; set; }
    public long EmployeeId { get; set; }
    public char? JobGroup { get; set; }
}
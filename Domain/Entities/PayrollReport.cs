using System;
namespace EmployeePayroll.Domain.Entities;

public class PayrollReport
{
    public List<EmployeeReport> EmployeeReports { get; set; } = new List<EmployeeReport>();
}
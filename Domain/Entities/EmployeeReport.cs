using System;
namespace EmployeePayroll.Domain.Entities;

public class EmployeeReport
{
    public long EmployeeId { get; set; }
    public PayPeriod PayPeriod { get; set; } = new PayPeriod();
    public decimal AmountPaid { get; set; }
}
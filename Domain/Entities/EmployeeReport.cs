using System;
namespace EmployeePayroll.Domain.Entities;

public class EmployeeReport
{
    public long EmployeeId { get; set; }
    public PayPeriod? PayPeriod { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal AmountPaid { get; set; }
}
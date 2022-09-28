using System;
namespace EmployeePayroll.Domain.Entities;

public class EmployeeReport
{
    public long EmployeeId { get; }
    public PayPeriod PayPeriod { get; }
    public decimal AmountPaid { get; }

    public EmployeeReport(long employeeId, PayPeriod payPeriod, decimal amountPaid)
    {
        EmployeeId = employeeId;
        PayPeriod = payPeriod;
        AmountPaid = amountPaid;
    }
}
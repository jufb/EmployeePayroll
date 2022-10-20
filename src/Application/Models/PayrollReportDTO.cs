using System;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Application.Models;

public class PayrollReportDTO
{
    public PayrollReport PayrollReport { get; set; } = new PayrollReport();
}
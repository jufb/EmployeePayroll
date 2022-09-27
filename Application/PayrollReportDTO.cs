using System;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Application
{
    public class PayrollReportDTO
    {
        public List<EmployeeReport> EmployeeReports { get; set; } = new List<EmployeeReport>();
    }
}
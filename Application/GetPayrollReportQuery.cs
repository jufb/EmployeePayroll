using System;
using System.Globalization;
using EmployeePayroll.Domain.Entities;
using EmployeePayroll.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll.Application;

public class GetPayrollReportQuery
{
    private readonly TimeReportContext _context;

    public GetPayrollReportQuery(TimeReportContext context)
    {
        _context = context;
    }

    public async Task<PayrollReportDTO> GetQuery()
    {
        IEnumerable<TimeReportDTO> timeReports = await new GetTimeReportsQuery(_context).GetQuery();

        PayrollReportDTO payrollReportDTO = new PayrollReportDTO();
        EmployeeReport employeeReport = new EmployeeReport();

        foreach (TimeReportDTO time in timeReports)
        {
            //Define the range of dates
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (time.Date.Day >= 1 && time.Date.Day <= 15)
            {
                startDate = new DateTime(time.Date.Year, time.Date.Month, 1);
                endDate = startDate.AddDays(14);
            }
            else if (time.Date.Day >= 16)
            {
                startDate = new DateTime(time.Date.Year, time.Date.Month, 1).AddDays(15);
                endDate = new DateTime(time.Date.Year, time.Date.Month, 1).AddMonths(1).AddSeconds(-1);
            }

            if (employeeReport.EmployeeId == 0) //if first time running
            {
                employeeReport.EmployeeId = time.EmployeeId;
                employeeReport.PayPeriod.StartDate = startDate;
            }
            
            if ((employeeReport.EmployeeId != time.EmployeeId) || (time.Date.Month != employeeReport.PayPeriod.StartDate.Month) || (startDate != employeeReport.PayPeriod.StartDate))
            {
                payrollReportDTO.EmployeeReports.Add(employeeReport);

                employeeReport = new EmployeeReport();
            }

            if (time.JobGroup.Equals('A'))
            {
                employeeReport.AmountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_A);
            }
            else if (time.JobGroup.Equals('B'))
            {
                employeeReport.AmountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_B);
            }

            employeeReport.EmployeeId = time.EmployeeId;
            employeeReport.PayPeriod.StartDate = startDate;
            employeeReport.PayPeriod.EndDate = endDate;
        }

        return payrollReportDTO;
    }
}
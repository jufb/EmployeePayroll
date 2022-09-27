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
            PayPeriod payPeriod = DefinePayPeriod(time.Date);

            if (employeeReport.EmployeeId.Equals(0)) //New employee
            {
                employeeReport.EmployeeId = time.EmployeeId;
                employeeReport.PayPeriod.StartDate = payPeriod.StartDate;
            }

            //Add the employeeReport into the list at every different employee or date
            if ((employeeReport.EmployeeId != time.EmployeeId) || (payPeriod.StartDate != employeeReport.PayPeriod.StartDate))
            {
                payrollReportDTO.PayrollReport.EmployeeReports.Add(employeeReport);

                employeeReport = new EmployeeReport();
            }

            CalculateAmountPaid(employeeReport, time);

            employeeReport.EmployeeId = time.EmployeeId;
            employeeReport.PayPeriod.StartDate = payPeriod.StartDate;
            employeeReport.PayPeriod.EndDate = payPeriod.EndDate;
        }

        return payrollReportDTO;
    }

    private PayPeriod DefinePayPeriod(DateTime time)
    {
        PayPeriod payPeriod = new PayPeriod();

        if (time.Date.Day >= 1 && time.Date.Day <= 15)
        {
            payPeriod.StartDate = new DateTime(time.Date.Year, time.Date.Month, 1).Date;
            payPeriod.EndDate = new DateTime(time.Date.Year, time.Date.Month, 1).AddDays(14).Date;
        }
        else if (time.Date.Day >= 16)
        {
            payPeriod.StartDate = new DateTime(time.Date.Year, time.Date.Month, 1).AddDays(15).Date;
            payPeriod.EndDate = new DateTime(time.Date.Year, time.Date.Month, 1).AddMonths(1).AddSeconds(-1).Date;
        }

        return payPeriod;
    }

    private static void CalculateAmountPaid(EmployeeReport employeeReport, TimeReportDTO time)
    {
        if (time.JobGroup.Equals('A'))
        {
            employeeReport.AmountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_A);
        }
        else if (time.JobGroup.Equals('B'))
        {
            employeeReport.AmountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_B);
        }
    }
}
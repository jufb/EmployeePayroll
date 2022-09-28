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
        long employeeId = 0;
        PayPeriod payPeriod = new PayPeriod();
        decimal amountPaid = 0;

        foreach (TimeReportDTO time in timeReports)
        {
            PayPeriod payPeriodDefined = DefinePayPeriod(time.Date);

            if (employeeId.Equals(0)) //New employee
            {
                employeeId = time.EmployeeId;
                payPeriod.StartDate = payPeriodDefined.StartDate;
            }

            //Add the employeeReport into the list at every different employee or date
            if ((employeeId != time.EmployeeId) || (payPeriodDefined.StartDate != payPeriod.StartDate))
            {
                payrollReportDTO.PayrollReport.EmployeeReports.Add(new EmployeeReport(employeeId, payPeriod, amountPaid));
                amountPaid = 0;
                payPeriod = new PayPeriod();
                employeeId = 0;
            }

            amountPaid = CalculateAmountPaid(amountPaid, time);
            employeeId = time.EmployeeId;
            payPeriod.StartDate = payPeriodDefined.StartDate;
            payPeriod.EndDate = payPeriodDefined.EndDate;
        }

        return payrollReportDTO;
    }

    private static decimal CalculateAmountPaid(decimal amountPaid, TimeReportDTO time)
    {
        if (time.JobGroup.Equals('A'))
        {
            amountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_A);
        }
        else if (time.JobGroup.Equals('B'))
        {
            amountPaid += Convert.ToDecimal(time.HoursWorked * TimeReport.PAY_GROUP_B);
        }

        return amountPaid;
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
}
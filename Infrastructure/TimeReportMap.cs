using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using EmployeePayroll.Domain.Entities;
using Newtonsoft.Json;

namespace EmployeePayroll.Infrastructure;

public class TimeReportMap : ClassMap<TimeReport>
{
    public TimeReportMap()
    {
        Map(m => m.Date).Name("date").TypeConverterOption.Format("d/M/yyyy");
        Map(m => m.EmployeeId).Name("employee id");
        Map(m => m.HoursWorked).Name("hours worked");
        Map(m => m.JobGroup).Name("job group");
    }
}
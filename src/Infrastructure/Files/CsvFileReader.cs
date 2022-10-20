using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using EmployeePayroll.Domain.Entities;

namespace EmployeePayroll.Infrastructure.Files;

public class CsvFileReader
{
    public static IEnumerable<TimeReport> LoadCsv(Stream file)
    {
        try
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<TimeReportMap>();
                var records = csv.GetRecords<TimeReport>().ToList();

                return records;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
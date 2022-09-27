# Employee Payroll
An API made in C# that posts csv format file and returns a payroll report of employees.

## Technologies used
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [CsvHelper](https://joshclose.github.io/CsvHelper/)

## Instructions

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Extract the file `employeepayroll.bundle`
3. Using terminal, navigate to `EmployeePayroll` and launch the project using `dotnet run`
4. Open Postman and make a `POST` request using the url `https://localhost:7150/api/timereport`
5. Add Parameter key `file` and Value as the url of the file time-report-x.csv where `x` is the reportId.

## How did you test that your implementation was correct?
  1. Implemented a test case.
  2. Used Postman and created test scenarios of requests using the data to understand the process and how would I implement it.
  3. Tested alternative cases using different data types and volumes.
  4. Implemented the presented solution following the requirements.
  5. Structured the code following some clean architecture principles.
  6. Did a regression test to guarantee the application was working as intended.

## If this application was destined for a production environment, what would you add or change?
  1. I would change the in-memory database to a real database.
  2. I would choose Postgres database due to the potential large data volume and to handle better performance.
  3. I would add more options to increase the performance and improve user experience such as:
      1. Pagination.
      2. Limit data per page.
      3. Filters to search employees in determined Date range and/or Amount Paid range.
      4. Display the Name of the Employee as well since just the Id don’t tell much. I would join the Employee data using employeeId. When displaying the results, there’s a possibility to know the details of the employee using a modal screen or a tooltip.
      5. Allow sorting by Employee, Start Date, End Date, or Amount Paid.

## What compromises did you have to make as a result of the time constraints of this challenge?
  * Used an in-memory database sacrificing performance and data permanence.
  * Not implemented authorization token.
  * Not implemented limitation of requests.
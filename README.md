# Employee Payroll
An API made in React and C# that posts csv format file and returns a payroll report of employees.

## Technologies used
* [React Bootstrap](https://react-bootstrap.github.io)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [CsvHelper](https://joshclose.github.io/CsvHelper/)

## Instructions
1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [React-Bootstrap](https://react-bootstrap.github.io/getting-started/introduction) via `npm` or `yarn`
   ```
   npm install react-bootstrap bootstrap
   ```
3. Navigate to `EmployeePayroll` and launch the back-end project using `dotnet run`
4. Navigate to `EmployeePayroll > src > WebUI > ClientApp` and launch the front-end project using `npm start`

### Testing the back-end only
1. Open Postman and make a `POST` request using the url `https://localhost:7150/api/timereport`
2. Add Body form-data key `file` as type `File` and Value as the csv file located in `EmployeePayroll > src > WebUI` time-report-x.csv where `x` is the variant report id.
3. The application also allows `GET` requests using the same url.

## Steps taken to test the application
  1. Implemented a test case.
  2. Used Postman and created test scenarios of requests using the data to understand the process and how I would implement it.
  3. Tested alternative cases using different data types and volumes.
  4. Implemented the presented solution following the requirements.
  5. Structured the code following some clean architecture principles.
  6. Did a regression test to guarantee the application was working as intended.

## Plans to improve the application
  1. Add a flag variable to change from in-memory database to a real database.
  2. Add SQL Server or Postgres database due to the potential large data volume and to handle better performance.
  3. Implement limitation of requests per minute per user.
  4. Include logging for monitoring purposes such as log4net. Logging would be used for understanding the app usage levels, if the server needs to be scalled or the database is inappropriate.
  5. Add validation for users using different versions of apps that consume this api. For instance, if the request version differs, the HTTP response would be 406 Not Acceptable, requiring the user to install an updated app version.
  6. Add more options to increase the performance and improve user experience:
      1. Pagination.
      2. Limit data per page.
      3. Filters to search employees in determined Date range and/or Amount Paid range.
      4. Display the Name of the Employee as well since just the Id don’t tell much. I would join the Employee data using employeeId. When displaying the results, there’s a possibility to know the details of the employee using a modal screen or a tooltip.
      5. Allow sorting by Employee, Start Date, End Date, or Amount Paid.
  7. Use a Swagger page for documentation purposes.
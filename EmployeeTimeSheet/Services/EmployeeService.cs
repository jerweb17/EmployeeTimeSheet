namespace EmployeeTimeSheet.Services
{
    using System.Globalization;
    using CsvHelper;
    using EmployeeTimeSheet.Models;

    public class EmployeeService
    {

        private readonly string _filePath;

        public EmployeeService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "Employees.csv");
        }

        public List<Employee> LoadEmployees()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Employee>().ToList();
        }

        public void SaveEmployees(List<Employee> employees)
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(employees);
        }
    }
}

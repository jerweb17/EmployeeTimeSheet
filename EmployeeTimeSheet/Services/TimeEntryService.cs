namespace EmployeeTimeSheet.Services
{
    using System.Globalization;
    using CsvHelper;
    using EmployeeTimeSheet.Models;
    public class TimeEntryService
    {
        private readonly string _filePath;

        public TimeEntryService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "TimeEntries.csv");
        }

        public List<TimeEntry> LoadTimeEntries()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TimeEntry>().ToList();
        }

        public void SaveTimeEntries(List<TimeEntry> timeEntries)
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(timeEntries);
        }


    }
}

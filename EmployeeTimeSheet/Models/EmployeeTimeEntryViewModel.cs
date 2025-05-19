namespace EmployeeTimeSheet.Models
{
    public class EmployeeTimeEntryViewModel
    {
        public int EntryID { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InTime { get; set; }

        public TimeOnly OutTime { get; set; }

    }
}

namespace EmployeeTimeSheet.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddTimeEntryViewModel
    {
        [Required]
        //I gave EmployeeID a display name here so that users could be selected by their names. 
        //The Employee Name gets translated into EmployeeID in the controller before submitting the POST
        [Display(Name = "Employee Name")]
        public int EmployeeID { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly InTime { get; set; }

        [Required]
        public TimeOnly OutTime { get; set; }

        // List to display in dropdown of add Time Entry screen
        public List<SelectListItem>? Employees { get; set; }
    }
}

// Controllers/TimeEntryController.cs
using EmployeeTimeSheet.Models;
using EmployeeTimeSheet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TimeEntryController : Controller
{
    private readonly EmployeeService _employeeService;
    private readonly TimeEntryService _timeEntryService;

    public TimeEntryController(EmployeeService employeeService, TimeEntryService timeEntryService)
    {
        _employeeService = employeeService;
        _timeEntryService = timeEntryService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        var employees = _employeeService.LoadEmployees()
            .Select(e => new SelectListItem
            {
                Value = e.EmployeeID.ToString(),     //Display the First and Last Name, but use the EmployeeID 
                Text = $"{e.FirstName} {e.LastName}"
            }).ToList();

        var model = new AddTimeEntryViewModel
        {
            Employees = employees,
            Date = DateOnly.FromDateTime(DateTime.Today) // Set default to today
        };
        return PartialView("_AddTimeEntryModal", model);
    }

    [HttpPost]
    public IActionResult Add(AddTimeEntryViewModel model)
    {
        var employees = _employeeService.LoadEmployees()
            .Select(e => new SelectListItem
            {
                Value = e.EmployeeID.ToString(),
                Text = $"{e.FirstName} {e.LastName}"
            }).ToList();
        model.Employees = employees;

        // Input Validation Section
        if (!ModelState.IsValid)
            return PartialView("_AddTimeEntryModal", model);

        // Custom validation: InTime before OutTime
        if (model.InTime >= model.OutTime)
        {
            ModelState.AddModelError("", "In Time must be before Out Time.");
            return PartialView("_AddTimeEntryModal", model);
        }

        // Custom validation: Only one entry per employee per date
        var existingEntries = _timeEntryService.LoadTimeEntries();
        if (existingEntries.Any(te => te.EmployeeID == model.EmployeeID && te.Date == model.Date))
        {
            ModelState.AddModelError("", "Only one time entry is allowed per employee per date.");
            return PartialView("_AddTimeEntryModal", model);
        }

        try
        {
            // Save new entry
            var newEntry = new TimeEntry
            {
                EntryID = existingEntries.Any() ? existingEntries.Max(te => te.EntryID) + 1 : 1,
                EmployeeID = model.EmployeeID,
                Date = model.Date,
                InTime = model.InTime,
                OutTime = model.OutTime
            };
            existingEntries.Add(newEntry);
            _timeEntryService.SaveTimeEntries(existingEntries);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            // Optionally log the error
            Console.WriteLine(ex.Message);
            return Json(new { success = false, error = "An error occurred while saving the time entry. Please try again." });
        }
    }
}

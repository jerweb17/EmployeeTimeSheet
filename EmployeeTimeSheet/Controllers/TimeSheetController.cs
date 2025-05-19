using EmployeeTimeSheet.Models;
using EmployeeTimeSheet.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeTimeSheet.Controllers
{
    public class TimeSheetController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly TimeEntryService _timeEntryService;

        public TimeSheetController(EmployeeService employeeService, TimeEntryService timeEntryService)
        {
            _employeeService = employeeService;
            _timeEntryService = timeEntryService;
        }

        public ActionResult Index(string lastName, DateOnly? date)
        {
            var employees = _employeeService.LoadEmployees();
            var timeEntries = _timeEntryService.LoadTimeEntries();

            var joined = from t in timeEntries
                         join e in employees on t.EmployeeID equals e.EmployeeID
                         select new EmployeeTimeEntryViewModel
                         {
                             EntryID = t.EntryID,
                             EmployeeID = e.EmployeeID,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             Date = t.Date,
                             InTime = t.InTime,
                             OutTime = t.OutTime
                         };

            // Filtering logic
            if (!string.IsNullOrWhiteSpace(lastName))
                joined = joined.Where(j => j.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
            if (date.HasValue)
                joined = joined.Where(j => j.Date == date.Value);

            // Pass filter values to the view using ViewData (optional, for sticky filters)
            ViewData["LastNameFilter"] = lastName;
            ViewData["DateFilter"] = date?.ToString("yyyy-MM-dd");
            return View(joined);
        }

        //I was not positive what would happen if I removed the routing that were injected when I created the controller, so I left them.
        
        //// GET: TimeSheetController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: TimeSheetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeSheetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeSheetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeSheetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeSheetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeSheetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeSheetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

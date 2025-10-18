using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeAgendamento.Services;
using SistemaDeAgendamento.Services.Models.Employee;
using SistemaDeAgendamento.Web.Enums;
using SistemaDeAgendamento.Web.Mappings;
using SistemaDeAgendamento.Web.Models.Employee;
using System.Net.WebSockets;

namespace SistemaDeAgendamento.Web.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("create")]
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            var model = new CreateViewModel
            {
                Name = "",
                Password = "",
                PhoneNumber = "",
                UserName = "",

                EmployeeAvailability = Enum.GetValues(typeof(WeekDay))
                .Cast<WeekDay>()
                .Select(day => new EmployeeAvailabilityViewModel
                {
                    WeekDay = day,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    IsActive = true
                })
                .ToList()
            };

            return View(model);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _employeeService.Create(model.MapToCreateEmployeeRequest());

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);

                return View(model);
            }

            return RedirectToAction("Read");
        }

        [Route("Read")]
        [Authorize(Roles = "Admin")]
        public IActionResult Read()
        {
            IList<EmployeeResult> employees;

            employees = _employeeService.Read();

            var model = new ReadViewModel 
            { 
                Employees = employees?.Select(c => c.MapToEmployeeViewModel()).ToList()
            };

            return View(model);
        }

        [Route("edit/{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var model = employee.MapToEditViewModel();

            return View(model);
             
        }

        [HttpPost("Edit/{id:int}")]
        [Authorize(Roles = "Admin")]
        
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _employeeService.Edit(model.MapToEditEmployeeRequest());

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);

                return View(model);
            }

            return RedirectToAction("Read");
        }

        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(EditViewModel model)
        {
            var result = _employeeService.Delete(model.Id);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);

                return View(model);
            }

            return RedirectToAction("Read");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Read");
        }
    }
}

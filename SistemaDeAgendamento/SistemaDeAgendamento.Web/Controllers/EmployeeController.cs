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
        [Authorize]
        public IActionResult Read()
        {
            IList<EmployeeResult> employees;

            var model = new ReadViewModel
            {
                Employees = null,
                EmployeeAvailability = new List<ReadEmployeeAvailabilityViewModel>()
            };

            if (User.IsInRole("Admin"))
            {
                employees = _employeeService.Read();

                model.Employees = employees?.Select(c => c.MapToEmployeeViewModel()).ToList();
            }
            else if (User.IsInRole("Employee"))
            {
                var userIdString = User.FindFirst("Id")?.Value;

                int userId = int.Parse(userIdString!);

                var employee = _employeeService.GetByUserId(userId);

                if (employee == null)
                {
                    return View(model);
                }

                model = employee.MapToReadViewModel();
            }

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

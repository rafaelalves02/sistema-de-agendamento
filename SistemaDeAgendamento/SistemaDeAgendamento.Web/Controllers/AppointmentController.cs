using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeAgendamento.Services;
using SistemaDeAgendamento.Web.Mappings;
using SistemaDeAgendamento.Web.Models.Appointment;

namespace SistemaDeAgendamento.Web.Controllers
{
    [Route("Appointment")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IServiceService _serviceService;
        private readonly IEmployeeService _employeeService;

        public AppointmentController(IAppointmentService appointmentService ,IServiceService serviceService, IEmployeeService employeeService)
        {
            _appointmentService = appointmentService;
            _serviceService = serviceService;
            _employeeService = employeeService;
        }

        [Route("Create/{serviceId:int}")]
        public IActionResult Create(int serviceId)
        {
            var service = _serviceService.GetById(serviceId);

            if (service == null)
            {
                return RedirectToAction("Read", "Service");
            }

            var model = new CreateViewModel
            {
                Client = new CreateClientViewModel
                {
                    Name = string.Empty,
                    PhoneNumber = string.Empty
                },
                Service = service.MapToAppointmentServiceViewModel(),
                Employees = GetEmployeeList()
            };

            return View(model);
        }

        [HttpPost("Create/{serviceId:int}")]

        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Employees = GetEmployeeList();
                return View(model);
            }

            var result = _appointmentService.Create(model.MapToCreateAppointmentRequest());

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);

                model.Employees = GetEmployeeList();

                return View(model);
            }

            return RedirectToAction("Read", "Service");
        }

        [HttpGet]
        public JsonResult GetAvailableTimes(int employeeId, DateTime date, int serviceDuration)
        {
            var times = _appointmentService.GetAvailableSlots(employeeId, date, serviceDuration);

            var formatted = times.Select(t => t.ToString(@"hh\:mm")).ToList();

            return Json(formatted);
        }

        public List<SelectListItem> GetEmployeeList()
        {
            return _employeeService.Read().Select(e => new SelectListItem(e.Name, e.Id.ToString())).ToList();
        }
    }
}

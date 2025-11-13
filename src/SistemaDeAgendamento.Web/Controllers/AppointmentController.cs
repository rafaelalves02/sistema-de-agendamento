using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services;
using SistemaDeAgendamento.Web.Mappings;
using SistemaDeAgendamento.Web.Models.Appointment;
using System.Text.RegularExpressions;
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

            Response.Cookies.Append("ClientPhone", model.Client!.PhoneNumber!, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(5),
                HttpOnly = true
            });

            return RedirectToAction("Read", "Service", new { success = true });
        }

        [Route("Read")]
        public IActionResult Read(ReadViewModel? model)
        {
            model = model ?? new ReadViewModel();

            var appointments = new List<AppointmentViewModel>();

            if (User.IsInRole("Admin"))
            {
                var result = _appointmentService.Read();

                appointments = result.Select(r => r.MapToAppointmentViewModel()).ToList();

                if (model.EmployeeId.HasValue)
                {                  
                   appointments = appointments.Where(a => a.Employee.Id == model.EmployeeId).ToList();
                }

                if (!string.IsNullOrWhiteSpace(model.ClientPhoneNumber))
                {
                    var filterDigits = Regex.Replace(model.ClientPhoneNumber, @"\D", "");
                    if (!string.IsNullOrEmpty(filterDigits))
                    {
                        appointments = appointments.Where(a =>
                        {
                            var clientPhone = a.Client?.PhoneNumber ?? string.Empty;
                            var clientDigits = Regex.Replace(clientPhone, @"\D", "");
                            return clientDigits.Contains(filterDigits);
                        }).ToList();
                    }
                }

                model.Employees = GetEmployeeList();
             
            }
            else if (User.IsInRole("Employee"))
            {
                var userId = Convert.ToInt32(User.Claims.First(c => c.Type == "Id").Value);

                if (userId == 0)
                {
                    return RedirectToAction("Login", "User");
                }

                var employeeId = _employeeService.GetByUserId(userId)?.Id ?? 0;

                if (employeeId == 0)
                {
                    return RedirectToAction("Login", "User");
                }

                var result = _appointmentService.GetByEmployeeId(employeeId);

                appointments = result.Select(r => r.MapToAppointmentViewModel()).ToList();

                if (!string.IsNullOrWhiteSpace(model.ClientPhoneNumber))
                {
                    var filterDigits = Regex.Replace(model.ClientPhoneNumber, @"\D", "");
                    if (!string.IsNullOrEmpty(filterDigits))
                    {
                        appointments = appointments.Where(a =>
                        {
                            var clientPhone = a.Client?.PhoneNumber ?? string.Empty;
                            var clientDigits = Regex.Replace(clientPhone, @"\D", "");
                            return clientDigits.Contains(filterDigits);
                        }).ToList();
                    }
                }
            }
            else
            {
                var clientPhoneNumber = model?.ClientPhoneNumber ?? string.Empty;

                clientPhoneNumber = Regex.Replace(clientPhoneNumber, @"\D", "");

                if (clientPhoneNumber == string.Empty && Request.Cookies.ContainsKey("ClientPhone"))
                {
                    clientPhoneNumber = Request.Cookies["ClientPhone"] ?? string.Empty;
                }

                if (clientPhoneNumber == string.Empty)
                {
                    return RedirectToAction(nameof(GetClientPhone));
                }

                var result = _appointmentService.GetByClientPhoneNumber(clientPhoneNumber);

                appointments = result.Select(r => r.MapToAppointmentViewModel()).ToList();

                model!.ClientPhoneNumber = clientPhoneNumber;
            }

            if (model!.Date.HasValue)
            {
                appointments = appointments.Where(a => a.StartTime.Date == model.Date.Value.Date).ToList();
            }

            if (model.ServiceId.HasValue)
            {
                appointments = appointments.Where(a => a.Service.Id == model.ServiceId.Value).ToList();
            }

            if (model.Status.HasValue)
            {
                appointments = appointments.Where(a => a.Status == model.Status.Value).ToList();
            }

            model.Services = GetServiceList();

            model.Appointments = appointments;

            return View(model);
        }

        [HttpGet]
        public JsonResult GetAvailableTimes(int employeeId, DateTime date, int serviceDuration)
        {
            var times = _appointmentService.GetAvailableSlots(employeeId, date, serviceDuration);

            var formatted = times.Select(t => t.ToString(@"hh\:mm")).ToList();

            return Json(formatted);
        }

        [Route("GetClientPhone")]
        public IActionResult GetClientPhone()
        {
            return View();
        }

        public List<SelectListItem> GetEmployeeList()
        {
            return _employeeService.Read().Select(e => new SelectListItem(e.Name, e.Id.ToString())).ToList();
        }

        private List<SelectListItem> GetServiceList()
        {
            return _serviceService.Read().Select(s => new SelectListItem(s.Name, s.Id.ToString())).ToList();
        }
    }
}

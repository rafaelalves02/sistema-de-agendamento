using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeAgendamento.Services;
using SistemaDeAgendamento.Services.Models.Service;
using SistemaDeAgendamento.Web.Mappings;
using SistemaDeAgendamento.Web.Models.Service;

namespace SistemaDeAgendamento.Web.Controllers
{
    [Route("service")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        [Route("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _serviceService.Create(model.MapToCreateServiceRequest());

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Não foi possível criar o serviço.");
                return View(model);
            }

            return RedirectToAction("Read");
        }

        [Route("Read")]
        public IActionResult Read(bool? fromHome)
        {
            IList<ServiceResult>? services = null;

            services = _serviceService.Read();

            var model = new ReadViewModel
            {
                Services = services?.Select(c => c.MapToServiceViewModel()).ToList(),
                ShowWelCome = fromHome
            };

            return View(model);
        }

        [Route("Edit/{id:int}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(int id)
        {
            var service = _serviceService.GetById(id);

            if (service == null)
            {
                return NotFound();
            }

            var model = service.MapToEditViewModel();

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

            var result = _serviceService.Edit(model.MapToEditServiceRequest());

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
            var result = _serviceService.Delete(model.Id);

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

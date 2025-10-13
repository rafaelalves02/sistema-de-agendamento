using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAgendamento.Web.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }


    }
}

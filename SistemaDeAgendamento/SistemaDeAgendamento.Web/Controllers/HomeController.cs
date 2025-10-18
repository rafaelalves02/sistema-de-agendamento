using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaDeAgendamento.Web.Models;

namespace SistemaDeAgendamento.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Read", "Service", new { fromHome = true });
        }
    }
}

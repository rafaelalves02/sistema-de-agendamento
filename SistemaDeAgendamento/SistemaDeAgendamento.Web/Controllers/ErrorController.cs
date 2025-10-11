using Microsoft.AspNetCore.Mvc;
using SistemaDeAgendamento.Web.Models.Error;
using System.Diagnostics;

namespace SistemaDeAgendamento.Web.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

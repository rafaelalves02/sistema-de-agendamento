using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SistemaDeAgendamento.Services;
using SistemaDeAgendamento.Web.Models.User;
using System.Security.Claims;

namespace SistemaDeAgendamento.Web.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userService.ValidateLogin(model.UserName, model.Password);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
                return View(model);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, result.User!.UserName),
                new (ClaimTypes.Role, result.User.Role.ToString()),
                new ("Id", result.User.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = model.RemerberMe
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Read", "Service");
        }
    }
}

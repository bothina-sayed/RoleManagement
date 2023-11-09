using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleManagement.Application.Abstractions;
using RoleManagement.Domain.ViewModels;

namespace RoleManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(loginDto);

                if (!result.Ok)
                {
                    ModelState.AddModelError("LoginError", result.Message);
                    HttpContext.Session.SetString("role", result.Data);
                    return View(loginDto);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(loginDto);
        }
    }
}

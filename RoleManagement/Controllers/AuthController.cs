using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleManagement.Application.Abstractions;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;

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
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //var x = new RegisterViewModel
            //{
            //    Email = "asmaa@gmail.com",
            //    GenderId = 3,
            //    Password = "P@ssW0rd",
            //    UnitId = 1,
            //    PhoneNumber = "01142827378",
            //    Name = "asmaa"
            //};
            //var result = await _authService.Register(x);
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
                    return View(loginDto);
                }

                HttpContext.Session.SetString("role", result.Data.Role);
                HttpContext.Session.SetString("userId", result.Data.Id.ToString());
                HttpContext.Session.SetString("Name" , result.Data.Name);

                return RedirectToAction("Index", "Home");
            }
            return View(loginDto);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(registerDto);

                if (!result.Ok)
                {
                    ModelState.AddModelError("RegisterError", result.Message);


                    return View(registerDto);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(registerDto);
        }
    }
}

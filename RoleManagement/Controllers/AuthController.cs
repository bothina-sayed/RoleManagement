using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleManagement.Application.Abstractions;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;
using System.Security.Claims;

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
        public IActionResult Login()
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
                    return View(loginDto);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Data.Name),
                    new Claim(ClaimTypes.Role, result.Data.Role),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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

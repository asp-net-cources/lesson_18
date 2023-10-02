using System.Security.Claims;
using Lesson18.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Lesson18.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginInputModel input)
    {
        if (input.Login == "user" && input.Password == "123")
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, input.Login)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        ModelState.AddModelError("Unathorized", "Неверные логин или пароль");
        return View(ModelState);
    }
}
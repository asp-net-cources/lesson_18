using System.Security.Claims;
using Lesson18.Data.EF;
using Lesson18.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Lesson18.Controllers;

public class AccountController : Controller
{
    private readonly ShopDbContext _dbContext;

    public AccountController(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginInputModel input)
    {
        var user = _dbContext.Users.FirstOrDefault(user =>
            string.Equals(user.Login, input.Login, StringComparison.OrdinalIgnoreCase)
            && string.Equals(user.Password, input.Password, StringComparison.Ordinal));
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
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
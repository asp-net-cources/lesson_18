using System.Security.Claims;
using Lesson18.Data.EF;
using Lesson18.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson18.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ShopDbContext _dbContext;

    public AccountController(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromForm]LoginInputModel input, [FromQuery]string? returnUrl)
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
                new(ClaimTypes.Role, user.Role.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect(returnUrl ?? "/");
        }
        
        ModelState.AddModelError("Unathorized", "Неверные логин или пароль");
        return View(ModelState);
    }

    [HttpGet]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
    
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
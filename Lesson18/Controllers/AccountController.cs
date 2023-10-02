using Lesson18.Models.Account;
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
    public ActionResult Login(LoginInputModel input)
    {
        if (input.Login == "user" && input.Password == "123")
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        ModelState.AddModelError("Unathorized", "Неверные логин или пароль");
        return View(ModelState);
    }
}
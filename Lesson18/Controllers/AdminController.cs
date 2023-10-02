using Lesson18.Data.EF;
using Lesson18.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson18.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ShopDbContext _dbContext;

    public AdminController(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ActionResult> Users()
    {
        var users = await _dbContext.Users.ToArrayAsync();
        var model = new UsersModel { Users = users };
        
        return View(model);
    }
}
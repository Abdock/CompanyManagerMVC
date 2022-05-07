using System.Security.Claims;
using CompanyManagerMVC.Context;
using CompanyManagerMVC.Entities;
using CompanyManagerMVC.Services;
using CompanyManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace CompanyManagerMVC.Controllers;

public class CustomerController : Controller
{
    private readonly MyDbContext _database;
    private readonly EntityFactory _entityFactory;

    public CustomerController(MyDbContext database, EntityFactory entityFactory)
    {
        _database = database;
        _entityFactory = entityFactory;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Tasks()
    {
        return View(await _database.Users
            .Where(u => u.Email == User.Identity!.Name)
            .Include(user => user.TaskExecutors)
            .Include(user => user.Department)
            .Include(user => user.Role)
            .FirstOrDefaultAsync());
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };
        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _database.Users
                .Where(user => user.Email == model.Email && user.Password == model.Password)
                .Include(user => user.Role)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                await Authenticate(user);

                return RedirectToAction("Tasks");
            }

            ModelState.AddModelError("", "Email or password incorrect");
        }

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "admin, moderator")]
    public async Task<ActionResult> Registration()
    {
        ViewBag.Departments = await _database.Departments.ToListAsync();
        ViewBag.Roles = await _database.UserRoles.ToListAsync();
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin, moderator")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Registration(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _database.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (user == null)
            {
                user = _entityFactory.CreateUser(model);
                await _database.Users.AddAsync(user);
                await _database.SaveChangesAsync();
                await Authenticate(user);

                return RedirectToAction("Tasks");
            }

            ModelState.AddModelError("", "Something is wrong");
        }

        ViewBag.Departments = await _database.Departments.ToListAsync();
        ViewBag.Roles = await _database.UserRoles.ToListAsync();
        return View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
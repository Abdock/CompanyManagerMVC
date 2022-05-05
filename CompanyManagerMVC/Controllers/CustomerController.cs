using CompanyManagerMVC.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerMVC.Controllers;

public class CustomerController : Controller
{
    private readonly CompanyContext _database;

    public CustomerController(CompanyContext database)
    {
        _database = database;
    }

    [Authorize]
    public async Task<ActionResult> Index()
    {
        return View(await _database.Users
            .Where(u => u.Email == User.Identity!.Name)
            .Include(user => user.TaskExecutors)
            .Include(user => user.Department)
            .Include(user => user.Role)
            .FirstOrDefaultAsync());
    }
}
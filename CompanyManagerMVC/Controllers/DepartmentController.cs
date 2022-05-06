using CompanyManagerMVC.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerMVC.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly MyDbContext _database;

    public DepartmentController(ILogger<DepartmentController> logger, MyDbContext database)
    {
        _logger = logger;
        _database = database;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_database.Departments
            .Include(department => department.Users)
            .ToList());
    }
}
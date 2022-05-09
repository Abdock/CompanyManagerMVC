using CompanyManagerMVC.Context;
using CompanyManagerMVC.Services;
using CompanyManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerMVC.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly MyDbContext _database;
    private readonly EntityFactory _entityFactory;

    public DepartmentController(ILogger<DepartmentController> logger, MyDbContext database, EntityFactory entityFactory)
    {
        _logger = logger;
        _database = database;
        _entityFactory = entityFactory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_database.Departments
            .Include(department => department.Users)
            .ToList());
    }

    [HttpGet]
    [Authorize]
    public IActionResult CreatePost()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreatePost(PostViewModel model)
    {
        if (ModelState.IsValid)
        {
            _entityFactory.CreatePost(model);
        }

        return View(model);
    }
}
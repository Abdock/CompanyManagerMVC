﻿using CompanyManagerMVC.DatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagerMVC.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly CompanyContext _database;

    public DepartmentController(ILogger<DepartmentController> logger, CompanyContext database)
    {
        _logger = logger;
        _database = database;
    }

    public IActionResult Index()
    {
        return View(_database.Departments.ToList());
    }
}
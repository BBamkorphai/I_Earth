using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;

namespace I_EARTH.Controllers;

public class RegisterController : Controller
{
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(ILogger<RegisterController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
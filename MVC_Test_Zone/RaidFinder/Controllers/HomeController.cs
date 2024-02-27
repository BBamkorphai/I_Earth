using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;

namespace I_EARTH.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    //dummy leader
    //[HttpGet]

    //[HttpPost]
    //public IActionResult Privacy([FromBody] RaidingPostModels model)
    //{
    //    Console.WriteLine(model.RaidingName + model.PowerLevel + model.PartyMaxSize + model.LeaderIP + model.RaidingTime);
    //    return Ok(model);
    //}
}

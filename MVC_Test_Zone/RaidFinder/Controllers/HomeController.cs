using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using I_EARTH.Models;

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
    [HttpGet]
    public IActionResult dummy_leader()
    {
        var partymember = new PartyMember{ ID= "1111", Power = 5000};
        Console.WriteLine(partymember.ID +" "+ partymember.Power);
        return Ok(partymember);
    }

    [HttpPost]
    public IActionResult Privacy([FromBody] RaidingPostModels model)
    {
        Console.WriteLine(model.RaidingName + model.PowerLevel + model.PartyMaxSize + model.LeaderIP + model.RaidingTime);
        return Ok(model);
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}

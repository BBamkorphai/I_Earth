using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;

namespace I_EARTH.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
<<<<<<< HEAD
        _logger = logger;
=======
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Description()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult PlayerStat()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }

>>>>>>> a5743e09149823e9cd1cee98a32ab56a8d4efb3c
    }

    public IActionResult Index()
    {
        UserDB.UpdateDB();
        IndexModels.UpdatePostDB();
        var Posts = IndexModels.GetPosts();

        return View(Posts);
    }

    public IActionResult AddPost()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddPost(RaidingPostModels post)
    {
        IndexModels.AddPost(post);
        return RedirectToAction("Index");
    }

    public IActionResult EditPost(int? id)
    {
        var post = IndexModels.GetPostCopyById(id.HasValue ? id.Value : 0);

        return View(post);
    }

    [HttpPost]
    public IActionResult EditPost(RaidingPostModels post)
    {

        IndexModels.UpdatePost(post.PostId, post);
        return RedirectToAction("Index");
    }
    public IActionResult DeletePost(int? id)
    {
        if (!id.HasValue) { return RedirectToAction("Index"); }
        IndexModels.DeletePost(id.Value);
        return RedirectToAction("Index");
    }

    public IActionResult RoomInfo(int? id)
    {
        UserDB.UpdateDB();
        IndexModels.UpdatePostDB();
        var post = IndexModels.GetPostCopyById(id.HasValue ? id.Value : 0);

        return View(post);
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

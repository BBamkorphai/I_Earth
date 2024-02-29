﻿using System.Diagnostics;
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
    //dummy leader
    //[HttpGet]

    //[HttpPost]
    //public IActionResult Privacy([FromBody] RaidingPostModels model)
    //{
    //    Console.WriteLine(model.RaidingName + model.PowerLevel + model.PartyMaxSize + model.LeaderIP + model.RaidingTime);
    //    return Ok(model);
    //}
}

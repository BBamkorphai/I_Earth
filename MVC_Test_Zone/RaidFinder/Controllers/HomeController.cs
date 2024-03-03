using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;
using Microsoft.AspNetCore.Http;
using System.Net.NetworkInformation;

namespace I_EARTH.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor contxt;

    public HomeController(IHttpContextAccessor httpContextAccessor)
    {
        contxt = httpContextAccessor;
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
    public IActionResult AddPost(RaidingPostModels post, int Hour, int Minute)
    {
        post.TimeOut = DateTime.Now.AddHours(Hour).AddMinutes(Minute);
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
    public IActionResult JoinRoom(int? PostId)
    {
        var UserId = contxt.HttpContext.Session.GetInt32("UserId");
        var post = IndexModels.GetPostCopyById(PostId.HasValue ? PostId.Value : 0);
        if ((post.PartyList.FirstOrDefault(x => x.UserId == UserId) != null) || (contxt.HttpContext.Session.GetInt32("UserId") == 0) || post.PartyList.Count == post.MaxSize)
        {
            return NoContent();
        }
        post.PartyList.Add(UserDB.GetUserCopyById((int)contxt.HttpContext.Session.GetInt32("UserId")));
        IndexModels.UpdatePost((int)PostId, post);
        UserDB.UpdateDB();
        IndexModels.UpdatePostDB();
        return RedirectToAction("RoomInfo", "Home", new { id = PostId });
    }

    public IActionResult KickUser(int? PostId, int? UserId)
    {
        var post = IndexModels.GetPostCopyById(PostId.HasValue ? PostId.Value : 0);
        post.PartyList.Remove((post.PartyList.FirstOrDefault(x => x.UserId == UserId)));
        IndexModels.UpdatePost((int)PostId, post);
        UserDB.UpdateDB();
        IndexModels.UpdatePostDB();
        return RedirectToAction("RoomInfo", "Home", new { id = PostId });
    }
    public IActionResult RoomInfo(int? id)
    {
        UserDB.UpdateDB();
        IndexModels.UpdatePostDB();
        var post = IndexModels.GetPostCopyById(id.HasValue ? id.Value : 0);

        return View(post);
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(Auth auth)
    {
        AuthDB.UpdateDB();
        var id = AuthDB.Authentication(auth);
        contxt.HttpContext.Session.SetInt32("UserId",id);
        if (id > 0)
        {
            auth.UserId = id;
            return RedirectToAction("test","User",auth);
        }
        return View();
    }

    public IActionResult Profile()
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

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
		IndexModels.UpdatePostDB();
		post.TimeOut = DateTime.Now.AddHours(Hour).AddMinutes(Minute);
        if (post.OwnerId == null)
        {
            post.OwnerId = 0;
        }
        IndexModels.AddPost(post);
        return RedirectToAction("Index");
    }

    public IActionResult EditPost(int? id)
    {
		IndexModels.UpdatePostDB();
		var post = IndexModels.GetPostCopyById(id.HasValue ? id.Value : 0);
        var userIds = post.PartyList.Select(user => user.UserId.ToString()).ToList();
        ViewBag.PartyList = string.Join(",", userIds);
        return View(post);
    }

    [HttpPost]
    public IActionResult EditPost(RaidingPostModels post, int Hour, int Minute, String Users)
    {
        var userIds = Users.Split(',').Select(int.Parse).ToList();
        foreach (var userId in userIds)
        {
            post.PartyList.Add(UserDB.GetUserCopyById(userId));
        }
        post.TimeOut = DateTime.Now.AddHours(Hour).AddMinutes(Minute);
        IndexModels.UpdatePost(post.PostId, post);
        return RedirectToAction("RoomInfo", "Home", new { id = post.PostId });
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
        var User = UserDB.GetUserCopyById((int)contxt.HttpContext.Session.GetInt32("UserId"));
        var post = IndexModels.GetPostCopyById(PostId.HasValue ? PostId.Value : 0);
        if ((post.PartyList.FirstOrDefault(x => x.UserId == UserId) != null) || (contxt.HttpContext.Session.GetInt32("UserId") == 0) || post.PartyList.Count == post.MaxSize || post.PowerLevel > User.Stat.PowerLevel)
        {
            return NoContent();
        }
        post.PartyList.Add(User);
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
        var imagelist = new List<byte[]>();
        foreach (var User in post.PartyList)
        {
            imagelist.Add(ImageMethod.GetImageById(User.UserId));
		}
        var viewModel = new RoomInfoViewModel
        {
            Post = post,
            ImageList = imagelist,
        };
        return View(viewModel);
    }
    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Search(string query)
    {
        IndexModels.UpdatePostDB();
		if (query == "" || query == null    ) { return Json(IndexModels.GetPosts().ToList()); }
		var filteredPosts = IndexModels.GetPosts().Where(post => post.Name.ToLower().StartsWith(query.ToLower())).ToList();
        //foreach (var post in filteredPosts)
        //{
            
        //}
        
        var Jsonified = Json(filteredPosts);
        return Jsonified;
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

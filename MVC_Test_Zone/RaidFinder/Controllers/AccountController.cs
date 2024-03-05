using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;
namespace RaidFinder.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor contxt;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            contxt = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login(String Username, String Password)
        {
            AuthDB.UpdateDB();
            Auth auth = new Auth();
            auth.Username = Username;
            auth.Password = Password;
            var id = AuthDB.Authentication(auth);
            contxt.HttpContext.Session.SetInt32("UserId", id);
            if (id > 0)
            {
                return RedirectToAction("test", "User");
            }
            return View();
        }
        public IActionResult Logout()
        {
            contxt.HttpContext.Session.SetInt32("UserId", 0);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string Username, string Password)
        {
            Auth auth = new Auth();
            auth.Username = Username;
            auth.Password = Password;
            AuthDB.AddUser(auth);
            return RedirectToAction("index");
        }

    }
}

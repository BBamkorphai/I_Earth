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
            contxt.HttpContext.Session.SetInt32("UserId", 0);
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
            contxt.HttpContext.Session.SetInt32("UserId", id);
            if (id > 0)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            contxt.HttpContext.Session.SetInt32("UserId", 0);
            return RedirectToAction("Index","Home");
        }
    }
}

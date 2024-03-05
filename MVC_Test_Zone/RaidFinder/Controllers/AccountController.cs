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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Auth auth)
        {
            if (ModelState.IsValid)
            {
                AuthDB.UpdateDB();
                var id = AuthDB.Authentication(auth);
                contxt.HttpContext.Session.SetInt32("UserId", id);
                if (id > 0)
                {
                    return RedirectToAction("test", "User");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            contxt.HttpContext.Session.SetInt32("UserId", 0);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(Auth auth) {
            AuthDB.AddUser(auth);
            return RedirectToAction("login");
        }
    }
}

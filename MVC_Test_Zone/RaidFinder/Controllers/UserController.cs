using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;
using System.Reflection;

namespace RaidFinder.Controllers
{
    public class UserController : Controller
    {
        public IActionResult ViewStat(int? id)
        {
            UserDB.UpdateDB();
            var Users = UserDB.GetUsers();

            return View(Users);
        }
        public IActionResult Profile(int? id)
        {
            UserDB.UpdateDB();
            var User = UserDB.GetUserCopyById((int)id);

            return View(User);
        }
        public IActionResult EditProfile(int? id)
        {
            var User = UserDB.GetUserCopyById(id.HasValue?id.Value:0);

            return View(User);
        }

        [HttpPost]
        public IActionResult EditStat(User user)
        {

            UserDB.UpdateUser(user.UserId, user);
            return RedirectToAction("Profile", "User", new { id = user.UserId });
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            UserDB.AddUser(user);
            return RedirectToAction("ViewStat");
        }

        public IActionResult DeleteUser(int? id) 
        { 
            if (!id.HasValue) { return RedirectToAction("Profile"); }
            UserDB.DeleteUser(id.Value);
            return RedirectToAction("Profile", "User", new { id = id });
        }
        public IActionResult test(Auth auth) {
            return View(auth);
        }

    }
}

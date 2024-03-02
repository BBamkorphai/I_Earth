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
        public IActionResult EditStat(int? id)
        {
            var User = UserDB.GetUserCopyById(id.HasValue?id.Value:0);

            return View(User);
        }

        [HttpPost]
        public IActionResult EditStat(User user)
        {

            UserDB.UpdateUser(user.UserId, user);
            return RedirectToAction("ViewStat");
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
            if (!id.HasValue) { return RedirectToAction("ViewStat"); }
            UserDB.DeleteUser(id.Value);
            return RedirectToAction("ViewStat");
        }
        public IActionResult test(Auth auth) {
            return View(auth);
        }

    }
}

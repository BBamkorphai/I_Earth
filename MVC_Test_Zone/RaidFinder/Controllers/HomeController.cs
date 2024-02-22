using Microsoft.AspNetCore.Mvc;

namespace RaidFinder.Controllers
{
    public class HomeController : Controller
    {
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

    }
}

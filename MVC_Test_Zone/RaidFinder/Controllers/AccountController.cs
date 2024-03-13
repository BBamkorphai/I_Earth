using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;
using System.Data.SqlClient;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
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
                return RedirectToAction("index", "Home");
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
        public async Task<IActionResult> Register(string Username, string Password)
        {
            Auth auth = new Auth();
            auth.Username = Username;
            auth.Password = Password;
            User user = new User();
            user.Name = Username;
            var model = new Image();
            model.OwnerId = UserDB.AddUser(user);
            AuthDB.AddUser(auth);
            
            model.ImageData = System.IO.File.ReadAllBytes(@"wwwroot/image/jhin.jpg");

            using (var connection = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("MERGE INTO Images AS target USING (VALUES (@OwnerId, @ImageData)) AS source (UserId, ImageData) ON target.UserId = source.UserId WHEN MATCHED THEN UPDATE SET target.ImageData = source.ImageData WHEN NOT MATCHED THEN INSERT (UserId, ImageData) VALUES (source.UserId, source.ImageData);", connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", model.OwnerId);
                    command.Parameters.AddWithValue("@ImageData", model.ImageData);

                    await command.ExecuteNonQueryAsync();
                }
            }
            return RedirectToAction("index");
        }

    }
}

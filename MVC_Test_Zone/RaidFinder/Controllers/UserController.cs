using Microsoft.AspNetCore.Mvc;
using RaidFinder.Models;
using System.Data.SqlClient;
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
        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(Image model, IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return RedirectToAction(nameof(Index));
			}

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				model.ImageData = memoryStream.ToArray();
			}

			using (var connection = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
			{
				await connection.OpenAsync();

				using (var command = new SqlCommand("INSERT INTO Images (UserId, ImageData) VALUES (@OwnerId, @ImageData)", connection))
				{
					command.Parameters.AddWithValue("@OwnerId", model.OwnerId);
					command.Parameters.AddWithValue("@ImageData", model.ImageData);

					await command.ExecuteNonQueryAsync();
				}
			}


			return RedirectToAction(nameof(Index));
		}

        private byte[] GetImageById(int? id)
        {
            byte[] image = null;
            using(var connection = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SELECT ImageData FROM Images WHERE UserId = @UserId", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    image = (byte[])cmd.ExecuteScalar();
                }
            }
            return image;
        }
		public IActionResult Profile(int? id)
        {
            UserDB.UpdateDB();
            var User = UserDB.GetUserCopyById((int)id);
            var Image = GetImageById(id);
            var viewModel = new ProfileViewModel
            {
                User = User,
                Image = Image
            };

            return View(viewModel);
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

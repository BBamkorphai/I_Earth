using System.Data.SqlClient;

namespace RaidFinder.Models
{
	public class ImageMethod
	{
		public static byte[] GetImageById(int? id)
		{
			byte[] image = null;
			using (var connection = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
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
	}
}

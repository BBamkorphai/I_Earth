namespace RaidFinder.Models
{
	public class User
	{
		public int UserId { get; set; } = 0;
		public string Name { get; set; } = string.Empty;
		public List<string> OwnedPostId { get; set; } = new List<string>();

		public Stat Stat { get; set; } = new Stat();
	}
}
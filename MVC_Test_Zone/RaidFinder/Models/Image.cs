using System.ComponentModel.DataAnnotations;

namespace RaidFinder.Models
{
	public class Image
	{
		public int OwnerId { get; set; }
		public byte[] ImageData { get; set; }
	}
}
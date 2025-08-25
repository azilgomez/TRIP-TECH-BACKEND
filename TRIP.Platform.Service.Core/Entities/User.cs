using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Entities
{
	public class User : EntityBase
	{
		[Key]
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ContactNumber { get; set; }
		public string UserType { get; set; }
		public int UserTypeId { get; set; }
	}
}

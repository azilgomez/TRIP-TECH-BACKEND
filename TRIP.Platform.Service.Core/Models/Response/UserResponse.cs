namespace TRIP.Platform.Service.Core.Models.Response
{
	public class UserResponse
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ContactNumber { get; set; }
		public string UserType { get; set; }
		public int UserTypeId { get; set; }
	}
}

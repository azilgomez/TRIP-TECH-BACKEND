using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class UserRequest
	{
		[Range(0, int.MaxValue)]
		public int? UserId { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Name { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string Password { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Email { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string ContactNumber { get; set; }
		[Range(0, int.MaxValue)]
		public int UserTypeId { get; set; }
	}
}

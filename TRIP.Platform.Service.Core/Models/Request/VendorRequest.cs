using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class VendorRequest
	{
		[Range(0, int.MaxValue)]
		public int? VendorId { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string CompanyName { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string VendorName { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Address { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Email { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string MobileNumber { get; set; }
		public string Type { get; set; }
		public bool IsActive { get; set; }
	}
}
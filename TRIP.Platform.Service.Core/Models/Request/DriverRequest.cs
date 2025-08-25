using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class DriverRequest
	{
		[Range(0, int.MaxValue)]
		public int? DriverId { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string DriverType { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string DriverName { get; set; }


		[StringLength(250, MinimumLength = 1)]
		public string Language { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Location { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string LicenseNumber { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public int ContactNumber { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public int Experience { get; set; }
	}
}

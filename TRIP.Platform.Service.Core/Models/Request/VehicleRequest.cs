using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class VehicleRequest
	{
		[Range(0, int.MaxValue)]
		public int? VehicleId { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string VehicleName { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string VehicleType { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string VehicleOwner { get; set; }

		[StringLength(250, MinimumLength = 1)]
		public string Amenities { get; set; }
		[Range(0, int.MaxValue)]
		public int VehicleYear { get; set; }
		[Range(0, int.MaxValue)]
		public int Capacity { get; set; }

		[Range(0, int.MaxValue)]
		public int Status { get; set; }
	}
}

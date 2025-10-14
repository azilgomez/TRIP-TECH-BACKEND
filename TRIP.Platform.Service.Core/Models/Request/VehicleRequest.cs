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
		public string VehicleClass { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string VehicleSeating { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string VehicleType { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string VehicleOwner { get; set; }
		[Range(0, int.MaxValue)]
		public int VehicleYear { get; set; }
		[Range(0, int.MaxValue)]
		public int Status { get; set; }
	}
}

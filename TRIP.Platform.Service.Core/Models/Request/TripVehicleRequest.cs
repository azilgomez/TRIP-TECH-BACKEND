using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class TripVehicleRequest
	{
		[Range(0, int.MaxValue)]
		public int? VendorId { get; set; }
		[Range(0, int.MaxValue)]
		public int? VehicleTypeId { get; set; }
		[Range(0, int.MaxValue)]
		public int? NumberOfVehicle { get; set; }
		public decimal? Cost { get; set; }
	}
}
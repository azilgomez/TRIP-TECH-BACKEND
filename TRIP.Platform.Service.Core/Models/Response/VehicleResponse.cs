using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Response
{
	public class VehicleResponse
	{
		[Key]
		public int VehicleId { get; set; }
		public string VehicleName { get; set; }
		public string VehicleType { get; set; }
		public string VehicleOwner { get; set; }
		public int VehicleYear { get; set; }
		public int Capacity { get; set; }
		public string Amenities { get; set; }
		public string Status { get; set; }
	}
}
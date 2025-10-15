using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Response
{
	public class DriverResponse
	{
		[Key]
		public int DriverId { get; set; }
		public string DriverType { get; set; }
		public string DriverName { get; set; }
		public int Language { get; set; }
		public string Location { get; set; }
		public string LicenseNumber { get; set; }
		public string ContactNumber { get; set; }
		public string Experience { get; set; }
		public int VendorId { get; set; }
	}
}

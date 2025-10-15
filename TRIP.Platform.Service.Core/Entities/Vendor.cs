namespace TRIP.Platform.Service.Core.Entities
{
	public class Vendor : EntityBase
	{
		public int? VendorId { get; set; }
		public string CompanyName { get; set; }
		public string VendorName { get; set; }
		public string  Address { get; set; }
		public string Email { get; set; }
		public string MobileNumber { get; set; }
		public string Type { get; set; }
	}
}
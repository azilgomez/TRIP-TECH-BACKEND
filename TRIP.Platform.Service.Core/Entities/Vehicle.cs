namespace TRIP.Platform.Service.Core.Entities
{
	public class Vehicle : EntityBase
	{
		public int? VehicleId { get; set; }
		public string VehicleName { get; set; }
		public string VehicleType { get; set; }
		public string VehicleOwner { get; set; }
		public string Amenities { get; set; }
		public int VehicleYear { get; set; }
		public int Capacity { get; set; }
		public int Status { get; set; }
	}
}
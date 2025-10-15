using System;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class TripDetailRequest
	{
		public int? TripId { get; set; }
		public string TripOwner { get; set; }
		public int? TripType { get; set; }
		public DateTime TripRequestedDate { get; set; }
		public string StartPlace { get; set; }
		public string EndPlace { get; set; }
		public DateTime StartDate { get; set; }
		public string Status { get; set; }
		public int? TripInCharge { get; set; }
		public string TripRemarks { get; set; }
		public string VendorRemarks { get; set; }
		public int DriverId { get; set; }
		public string ContactNumber { get; set; }
		public decimal? AdhocCost { get; set; }
		public decimal? VehicleCost { get; set; }
	}
}
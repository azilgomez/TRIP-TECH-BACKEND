using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class TripRequest
	{
		[Range(0, int.MaxValue)]
		public int? TripId { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string TripOwner { get; set; }
		[Range(0, int.MaxValue)]
		public string TripType { get; set; }
		public DateTime? TripRequestedDate { get; set; }
		public DateTime? StartDate { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string StartPlace { get; set; }
		[StringLength(250, MinimumLength = 1)]
		public string EndPlace { get; set; }
		[Range(0, int.MaxValue)]
		public int AssignTo { get; set; }
		[Range(0, int.MaxValue)]
		public int InChargePerson { get; set; }
		public string TripRemark { get; set; }
		public string VendorRemark { get; set; }
		public string DriverName { get; set; }
		public string DriverConactNumber { get; set; }
		public decimal? VehicleCost { get; set; }
		public decimal? AdhocCost { get; set; }
		[Range(0, int.MaxValue)]
		public int Status { get; set; }
	}
}
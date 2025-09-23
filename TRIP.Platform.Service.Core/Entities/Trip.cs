using System;

namespace TRIP.Platform.Service.Core.Entities
{
	public class Trip
	{
		public int? TripId { get; set; }
		public string TripOwner { get; set; }
		public string TripType { get; set; }
		public DateTime? TripRequestedDate { get; set; }
		public DateTime? StartDate { get; set; }
		public string StartPlace { get; set; }
		public string EndPlace { get; set; }
		public int? AssignTo { get; set; }
		public int? InChargePerson { get; set; }
		public string TripRemarks { get; set; }
		public int Status { get; set; }
	}
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Models.Response
{
	public class TripResponse
	{
		[Key]
		public int? TripId { get; set; }
		public string TripType { get; set; }
		public DateTime? TripDate { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string StartPlace { get; set; }
		public string EndPlace { get; set; }
		public int Status { get; set; }
	}
}

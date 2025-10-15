using System.Collections.Generic;

namespace TRIP.Platform.Service.Core.Models.Request
{
	public class TripRequest
	{
		public IEnumerable<TripDetailRequest> Trips { get; set; }
		public IEnumerable<TripVehicleRequest> Vehicles { get; set; }
	}
}

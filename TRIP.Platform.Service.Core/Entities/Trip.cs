using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TRIP.Platform.Service.Core.Models.Request;

namespace TRIP.Platform.Service.Core.Entities
{
	[Keyless]
	public class Trip : EntityBase
	{
		public IEnumerable<TripDetail> Trips { get; set; }
		public IEnumerable<TripVehicleRequest> Vehicles { get; set; }
	}
}
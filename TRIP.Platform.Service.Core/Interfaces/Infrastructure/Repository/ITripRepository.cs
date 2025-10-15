using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface ITripRepository : IRepository<Trip>
	{
		Task<IEnumerable<TripDetail>> GetTrips(string loggedUser, CancellationToken cancellationToken);
		Task<TripDetail> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken);
		Task<IEnumerable<TripVehicle>> GetTripVehiclesById(int tripId, string loggedUser, CancellationToken cancellationToken);
		Task<int> SaveTrip(Trip trip, string loggedUser, CancellationToken cancellationToken);
		Task<bool> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken);
	}
}

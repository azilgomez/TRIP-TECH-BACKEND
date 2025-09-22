using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface ITripRepository : IRepository<Trip>
	{
		Task<IEnumerable<Trip>> GetTrips(string loggedUser, CancellationToken cancellationToken);
		Task<Trip> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken);
		Task<bool> SaveTrip(Trip trip, string loggedUser, CancellationToken cancellationToken);
		Task<bool> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken);
	}
}

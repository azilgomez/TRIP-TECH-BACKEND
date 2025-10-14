using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface ITripService
	{
		Task<IEnumerable<TripResponse>> GetTrips(string loggedUser, CancellationToken cancellationToken);
		Task<TripResponse> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> SaveTrip(TripRequest trip, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken);
	}
}
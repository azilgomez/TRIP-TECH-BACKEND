using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;
using TRIP.Platform.Service.Core.Services;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface ITripService
	{
		Task<Response<bool>> SaveTrip(TripRequest trip, string loggedUser, CancellationToken cancellationToken);
	}
}
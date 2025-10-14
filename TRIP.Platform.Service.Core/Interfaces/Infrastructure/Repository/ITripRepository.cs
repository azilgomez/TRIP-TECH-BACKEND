using System.Threading.Tasks;
using System.Threading;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface ITripRepository : IRepository<Trip>
	{
		Task<int> SaveTrip(Trip trip, string loggedUser, CancellationToken cancellationToken);
	}
}

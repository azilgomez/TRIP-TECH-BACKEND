using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface IDriverRepository : IRepository<Driver>
	{
		Task<IEnumerable<Driver>> GetDrivers(string loggedUser, CancellationToken cancellationToken);
		Task<Driver> GetDriverById(int driverId, string loggedUser, CancellationToken cancellationToken);
		Task<bool> SaveDriver(Driver driver, string loggedUser, CancellationToken cancellationToken);
		Task<bool> DeleteDriver(int driverId, string loggedUser, CancellationToken cancellationToken);
	}
}

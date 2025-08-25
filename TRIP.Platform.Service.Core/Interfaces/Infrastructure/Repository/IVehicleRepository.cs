using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface IVehicleRepository : IRepository<Vehicle>
	{
		Task<IEnumerable<Vehicle>> GetVehicles(string loggedUser, CancellationToken cancellationToken);
		Task<Vehicle> GetVehicleById(int vehicleId, string loggedUser, CancellationToken cancellationToken);
		Task<bool> SaveVehicle(Vehicle vehicle, string loggedUser, CancellationToken cancellationToken);
		Task<bool> DeleteVehicle(int vehicleId, string loggedUser, CancellationToken cancellationToken);
	}
}

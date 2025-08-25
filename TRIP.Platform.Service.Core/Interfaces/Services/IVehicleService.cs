using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface IVehicleService
	{
		Task<IEnumerable<VehicleResponse>> GetVehicles(string loggedUser, CancellationToken cancellationToken);
		Task<VehicleResponse> GetVehicleById(int vehicleId, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> SaveVehicle(VehicleRequest vehicle, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> DeleteVehicle(int vehicleId, string loggedUser, CancellationToken cancellationToken);
	}
}
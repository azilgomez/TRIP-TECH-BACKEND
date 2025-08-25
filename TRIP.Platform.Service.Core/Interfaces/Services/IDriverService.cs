using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface IDriverService
	{
		Task<IEnumerable<DriverResponse>> GetDrivers(string loggedUser, CancellationToken cancellationToken);
		Task<DriverResponse> GetDriverById(int driverId, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> SaveDriver(DriverRequest driver, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> DeleteDriver(int driverId, string loggedUser, CancellationToken cancellationToken);
	}
}

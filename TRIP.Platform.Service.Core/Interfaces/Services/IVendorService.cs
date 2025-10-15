using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface IVendorService
	{
		Task<IEnumerable<VendorResponse>> GetVendors(string loggedUser, CancellationToken cancellationToken);
		Task<VendorResponse> GetVendorById(int vendorId, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> SaveVendor(VendorRequest vendor, string loggedUser, CancellationToken cancellationToken);
		Task<Response<bool>> DeleteVendor(int vendorId, string loggedUser, CancellationToken cancellationToken);
	}
}

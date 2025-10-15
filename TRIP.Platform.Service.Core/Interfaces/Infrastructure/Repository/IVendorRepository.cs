using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface IVendorRepository
	{
		Task<IEnumerable<Vendor>> GetVendors(string loggedUser, CancellationToken cancellationToken);
		Task<Vendor> GetVendorById(int vendorId, string loggedUser, CancellationToken cancellationToken);
		Task<bool> SaveVendor(Vendor vendor, string loggedUser, CancellationToken cancellationToken);
		Task<bool> DeleteVendor(int vendorId, string loggedUser, CancellationToken cancellationToken);
	}
}

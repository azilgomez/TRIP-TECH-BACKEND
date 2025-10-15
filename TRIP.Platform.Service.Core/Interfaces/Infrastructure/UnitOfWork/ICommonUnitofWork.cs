using System;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.UnitOfWork
{
	public interface ICommonUnitofWork : IDisposable
	{
		IUserRepository UserRepository { get; }
		IVehicleRepository VehicleRepository { get; }
		IDriverRepository DriverRepository { get; }
		ITripRepository TripRepository { get; }
		IVendorRepository VendorRepository { get; }
	}
}
using System;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.UnitOfWork;
using TRIP.Platform.Service.Infrastructure.DBContext;
using TRIP.Platform.Service.Infrastructure.Providers.Repository;

namespace TRIP.Platform.Service.Infrastructure.Providers.UnitOfWork
{
	public class CommonUnitOfWork : ICommonUnitofWork
	{
		private readonly TripDbContext _context;
		public IUserRepository UserRepository { get; }
		public IVehicleRepository VehicleRepository { get; }
		public IDriverRepository DriverRepository { get; }
		public ITripRepository TripRepository { get; }
		public CommonUnitOfWork(TripDbContext dbContext)
		{
			_context = dbContext;
			UserRepository = new UserRepository(_context);
			VehicleRepository = new VehicleRepository(_context);
			DriverRepository = new DriverRepository(_context);
			TripRepository = new TripRepository(_context);
		}
		private bool disposedValue = false; // To detect redundant calls
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					this._context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
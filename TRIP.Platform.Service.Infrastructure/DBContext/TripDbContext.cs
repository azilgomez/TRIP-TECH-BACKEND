using Microsoft.EntityFrameworkCore;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Infrastructure.DBContext
{
	public class TripDbContext : DbContext
	{
		public TripDbContext(DbContextOptions<TripDbContext> options) : base(options) { }
		public DbSet<User> User { get; set; }
		public DbSet<Vehicle> Vehicle { get; set; }
		public DbSet<Driver> Driver { get; set; }
	}
}
using Autofac;
using AutoMapper;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.UnitOfWork;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Services;
using TRIP.Platform.Service.Infrastructure.DBContext;
using TRIP.Platform.Service.Infrastructure.Providers.UnitOfWork;

namespace TRIP.Platform.Service.API
{
	public class AutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance(new AutoMapperConfiguration().Configure()).As<IMapper>();
			builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
			builder.RegisterType<VehicleService>().As<IVehicleService>().InstancePerLifetimeScope();
			builder.RegisterType<TripService>().As<ITripService>().InstancePerLifetimeScope();
			builder.RegisterType<DriverService>().As<IDriverService>().InstancePerLifetimeScope();
			builder.RegisterType<CommonUnitOfWork>().As<ICommonUnitofWork>().InstancePerLifetimeScope();
			builder.Register((x) => new CommonUnitOfWork(x.Resolve<TripDbContext>()));
			base.Load(builder);
		}
	}
}

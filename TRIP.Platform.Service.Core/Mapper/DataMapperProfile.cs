using AutoMapper;
using TRIP.Platform.Service.Core.Entities;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Mapper
{
	public class DataMapperProfile : Profile
	{
		public DataMapperProfile()
		{
			RegisterMapping();
		}
		private void RegisterMapping()
		{
			this.CreateMap(typeof(User), typeof(UserResponse));
			this.CreateMap(typeof(UserRequest), typeof(User));
			this.CreateMap(typeof(VehicleRequest), typeof(Vehicle));
			this.CreateMap(typeof(Vehicle), typeof(VehicleResponse));
			this.CreateMap(typeof(Driver), typeof(DriverResponse));
			this.CreateMap(typeof(DriverRequest), typeof(Driver));
		}
	}
}

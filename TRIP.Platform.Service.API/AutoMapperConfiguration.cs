using AutoMapper;
using System;
using System.Linq;
using TRIP.Platform.Service.Core.Mapper;

namespace TRIP.Platform.Service.API
{
	public class AutoMapperConfiguration
	{
		public IMapper Configure()
		{
			var profiles = AppDomain.CurrentDomain.GetAssemblies()
			  .SelectMany(s => s.GetTypes())
			  .Where(a => (
				typeof(DataMapperProfile).IsAssignableFrom(a)				
			  ));

			// Initialize AutoMapper with each instance of the profiles found.
			var mapperConfiguration = new MapperConfiguration(a => profiles.ToList().ForEach(a.AddProfile));

			return mapperConfiguration.CreateMapper();
		}
	}
}

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;
using TRIP.Platform.Service.Core.Interfaces.Infrastructure.UnitOfWork;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Services
{
	public class TripService : ITripService
	{
		private ICommonUnitofWork _commonUnitofWork;
		private IConfiguration _configuration;
		private ILogger<TripService> _logger;
		private readonly IMapper _mapper;
		public TripService(ICommonUnitofWork commonUnitofWork, IMapper mapper, IConfiguration configuration, ILogger<TripService> logger)
		{
			this._commonUnitofWork = commonUnitofWork ?? throw new ArgumentNullException(nameof(commonUnitofWork));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_configuration = configuration;
			_logger = logger;
		}

		/// <summary>
		/// Method to save trip
		/// </summary>
		/// <param name="trip"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> SaveTrip(TripRequest trip, string loggedUser, CancellationToken cancellationToken)
		{
			var addedTrip = this._mapper.Map<TripRequest, Trip>(trip);
			var result = await _commonUnitofWork.TripRepository.SaveTrip(addedTrip, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result > 0
			};
			return validation;
		}
	}
}
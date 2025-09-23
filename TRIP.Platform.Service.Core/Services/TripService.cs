using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		/// Method to save vehicle
		/// </summary>
		/// <param name="vehicle"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> SaveTrip(TripRequest trip, string loggedUser, CancellationToken cancellationToken)
		{
			var addedTrip = this._mapper.Map<TripRequest, Trip>(trip);
			var result = await _commonUnitofWork.TripRepository.SaveTrip(addedTrip, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to delete Trip
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> DeleteTrip(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			var result = await _commonUnitofWork.TripRepository.DeleteTrip(tripId, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to get all trips
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<TripResponse>> GetTrips(string loggedUser, CancellationToken cancellationToken)
		{
			var trips = await this._commonUnitofWork.TripRepository.GetTrips(loggedUser, cancellationToken);
			var result = this._mapper.Map<IEnumerable<Trip>, IEnumerable<TripResponse>>(trips);
			return result;
		}

		/// <summary>
		/// Method to get vehicle details by Id
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TripResponse> GetTripById(int tripId, string loggedUser, CancellationToken cancellationToken)
		{
			var trip = await this._commonUnitofWork.TripRepository.GetTripById(tripId, loggedUser, cancellationToken);
			var result = this._mapper.Map<Trip, TripResponse>(trip);
			return result;
		}
	}
}
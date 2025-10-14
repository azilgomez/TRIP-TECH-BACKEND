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
	public class VehicleService : IVehicleService
	{
		private ICommonUnitofWork _commonUnitofWork;
		private IConfiguration _configuration;
		private ILogger<VehicleService> _logger;
		private readonly IMapper _mapper;
		public VehicleService(ICommonUnitofWork commonUnitofWork, IMapper mapper, IConfiguration configuration, ILogger<VehicleService> logger)
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
		public async Task<Response<bool>> SaveVehicle(VehicleRequest vehicle, string loggedUser, CancellationToken cancellationToken)
		{
			var addedVehicle = this._mapper.Map<VehicleRequest, Vehicle>(vehicle);
			var result = await _commonUnitofWork.VehicleRepository.SaveVehicle(addedVehicle, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to delete vehicle
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> DeleteVehicle(int vehicleId, string loggedUser, CancellationToken cancellationToken)
		{
			var result = await _commonUnitofWork.VehicleRepository.DeleteVehicle(vehicleId, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to get all vehicles
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<VehicleResponse>> GetVehicles(string loggedUser, CancellationToken cancellationToken)
		{
			var vehicles = await this._commonUnitofWork.VehicleRepository.GetVehicles(loggedUser, cancellationToken);
			var result = this._mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResponse>>(vehicles);
			return result;
		}

		/// <summary>
		/// Method to get vehicle details by Id
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<VehicleResponse> GetVehicleById(int vehicleId, string loggedUser, CancellationToken cancellationToken)
		{
			var vehicle = await this._commonUnitofWork.VehicleRepository.GetVehicleById(vehicleId, loggedUser, cancellationToken);
			var result = this._mapper.Map<Vehicle, VehicleResponse>(vehicle);
			return result;
		}
	}
}
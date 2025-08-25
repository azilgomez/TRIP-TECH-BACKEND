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
	public class DriverService : IDriverService
	{
		private ICommonUnitofWork _commonUnitofWork;
		private IConfiguration _configuration;
		private ILogger<DriverService> _logger;
		private readonly IMapper _mapper;
		public DriverService(ICommonUnitofWork commonUnitofWork, IMapper mapper, IConfiguration configuration, ILogger<DriverService> logger)
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
		public async Task<Response<bool>> SaveDriver(DriverRequest driver, string loggedUser, CancellationToken cancellationToken)
		{
			var addedDriver = this._mapper.Map<DriverRequest, Driver>(driver);
			var result = await _commonUnitofWork.DriverRepository.SaveDriver(addedDriver, loggedUser, cancellationToken);
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
		public async Task<Response<bool>> DeleteDriver(int vehicleId, string loggedUser, CancellationToken cancellationToken)
		{
			var result = await _commonUnitofWork.DriverRepository.DeleteDriver(vehicleId, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to get all drivers
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<DriverResponse>> GetDrivers(string loggedUser, CancellationToken cancellationToken)
		{
			var drivers = await this._commonUnitofWork.DriverRepository.GetDrivers(loggedUser, cancellationToken);
			var result = this._mapper.Map<IEnumerable<Driver>, IEnumerable<DriverResponse>>(drivers);
			return result;
		}

		/// <summary>
		/// Method to get driver details by Id
		/// </summary>
		/// <param name="driverId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<DriverResponse> GetDriverById(int driverId, string loggedUser, CancellationToken cancellationToken)
		{
			var driver = await this._commonUnitofWork.DriverRepository.GetDriverById(driverId, loggedUser, cancellationToken);
			var result = this._mapper.Map<Driver, DriverResponse>(driver);
			return result;
		}
	}
}

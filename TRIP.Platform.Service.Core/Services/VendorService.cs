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
	public class VendorService : IVendorService
	{
		private ICommonUnitofWork _commonUnitofWork;
		private IConfiguration _configuration;
		private ILogger<VendorService> _logger;
		private readonly IMapper _mapper;
		public VendorService(ICommonUnitofWork commonUnitofWork, IMapper mapper, IConfiguration configuration, ILogger<VendorService> logger)
		{
			this._commonUnitofWork = commonUnitofWork ?? throw new ArgumentNullException(nameof(commonUnitofWork));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_configuration = configuration;
			_logger = logger;
		}
		/// <summary>
		/// Method to delete vendor
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> DeleteVendor(int vendorId, string loggedUser, CancellationToken cancellationToken)
		{
			var result = await _commonUnitofWork.VendorRepository.DeleteVendor(vendorId, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}

		/// <summary>
		/// Method to get vendor by Id
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<VendorResponse> GetVendorById(int vendorId, string loggedUser, CancellationToken cancellationToken)
		{
			var vendor = await this._commonUnitofWork.VendorRepository.GetVendorById(vendorId, loggedUser, cancellationToken);
			var result = this._mapper.Map<Vendor, VendorResponse>(vendor);
			return result;
		}

		/// <summary>
		/// Method to get vendors
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<VendorResponse>> GetVendors(string loggedUser, CancellationToken cancellationToken)
		{
			var drivers = await this._commonUnitofWork.VendorRepository.GetVendors(loggedUser, cancellationToken);
			var result = this._mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorResponse>>(drivers);
			return result;
		}

		/// <summary>
		/// Method to save vendor
		/// </summary>
		/// <param name="vendor"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> SaveVendor(VendorRequest vendor, string loggedUser, CancellationToken cancellationToken)
		{
			var addedVendor = this._mapper.Map<VendorRequest, Vendor>(vendor);
			var result = await _commonUnitofWork.VendorRepository.SaveVendor(addedVendor, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}
	}
}

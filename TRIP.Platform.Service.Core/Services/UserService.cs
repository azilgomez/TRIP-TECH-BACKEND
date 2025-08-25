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
	public class UserService : IUserService
	{
		private ICommonUnitofWork _commonUnitofWork;
		private IConfiguration _configuration;
		private ILogger<UserService> _logger;
		private readonly IMapper _mapper;
		public UserService(ICommonUnitofWork commonUnitofWork, IMapper mapper, IConfiguration configuration, ILogger<UserService> logger)
		{
			this._commonUnitofWork = commonUnitofWork ?? throw new ArgumentNullException(nameof(commonUnitofWork));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_configuration = configuration;
			_logger = logger;
		}
		
		/// <summary>
		/// Method to get all users
		/// </summary>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IEnumerable<UserResponse>> GetUsers(string loggedUser, CancellationToken cancellationToken)
		{
			var users = await this._commonUnitofWork.UserRepository.GetUsers(loggedUser, cancellationToken);
			var result = this._mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
			return result;
		}

		/// <summary>
		/// Method to get logged in user detail
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<UserResponse> GetLoggedInUserDetail(string email, string password, CancellationToken cancellationToken)
		{
			var users = await this._commonUnitofWork.UserRepository.GetLoggedInUserDetail(email, password, cancellationToken);
			var result = this._mapper.Map<User, UserResponse>(users);
			return result;
		}

		/// <summary>
		/// Method to save User details
		/// </summary>
		/// <param name="user"></param>
		/// <param name="loggedUser"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Response<bool>> SaveUser(UserRequest user, string loggedUser, CancellationToken cancellationToken)
		{
			var applicationUser = this._mapper.Map<UserRequest, User>(user);
			var result = await _commonUnitofWork.UserRepository.SaveUser(applicationUser, loggedUser, cancellationToken);
			Response<bool> validation = new Response<bool>
			{
				IsSuccess = result
			};
			return validation;
		}
	}
}

using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Users;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.API.Controllers
{
	public class UserController : BaseController
	{
		IWebHostEnvironment _hostingEnvironment;
		public UserController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(mediator, _httpContextAccessor)
		{
			_hostingEnvironment = environment;
		}

		/// <summary>
		/// Method to get all users
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetUsers")]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetUsersQuery(currentUser) { }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to get logged in user details
		/// </summary>
		/// <param name="email"></param>
		/// <param name="paswword"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetLoggedInUserDetail")]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetLoggedInUserDetail(string email, string password, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetLoggedInUserDetailQuery(currentUser) { Email = email, Password = password }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to save users
		/// </summary>
		/// <param name="userDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveUser")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveUser(UserRequest userDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertUserCommand() { UserDetail = userDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}

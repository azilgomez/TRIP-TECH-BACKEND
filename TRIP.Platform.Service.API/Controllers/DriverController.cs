using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Drivers;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.API.Controllers
{
	public class DriverController : BaseController
	{
		IWebHostEnvironment _hostingEnvironment;
		public DriverController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(mediator, _httpContextAccessor)
		{
			_hostingEnvironment = environment;
		}

		/// <summary>
		/// Method to all driver details
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetDrivers")]
		[ProducesResponseType(typeof(DriverResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDrivers(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetDriverQuery(currentUser) { }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to get driver by Id
		/// </summary>
		/// <param name="driverId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetDriverById")]
		[ProducesResponseType(typeof(DriverResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDriverById(int driverId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetDriverByIdQuery(currentUser) { DriverId = driverId }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to save driver
		/// </summary>
		/// <param name="vehicleDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveDriver")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveDriver(DriverRequest driverDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertDriverCommand() { DriverDetail = driverDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to delete driver
		/// </summary>
		/// <param name="driverId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("DeleteDriver")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteDriver(int driverId, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new DeleteDriverCommand() { DriverId = driverId, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}

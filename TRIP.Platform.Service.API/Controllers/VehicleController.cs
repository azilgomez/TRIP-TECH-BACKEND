using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Vehicles;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.API.Controllers
{
	public class VehicleController : BaseController
	{
		IWebHostEnvironment _hostingEnvironment;
		public VehicleController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(mediator, _httpContextAccessor)
		{
			_hostingEnvironment = environment;
		}

		/// <summary>
		/// Method to get all vehicles
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetVehicles")]
		[ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetVehicles(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetVehicleQuery(currentUser) { }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to get vehicle by Id
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetVehicleById")]
		[ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetVehicleById(int vehicleId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetVehicleByIdQuery(currentUser) { VehicleId = vehicleId }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to save vehicle
		/// </summary>
		/// <param name="vehicleDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveVehicle")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveVehicle(VehicleRequest vehicleDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertVehicleCommand() { VehicleDetail = vehicleDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to delete vehicle
		/// </summary>
		/// <param name="vehicleId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("DeleteVehicle")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteVehicle(int vehicleId, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new DeleteVehicleCommand() { VehicleId = vehicleId, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}
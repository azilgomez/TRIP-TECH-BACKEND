using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Trips;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.API.Controllers
{
	public class TripController : BaseController
	{
		IWebHostEnvironment _hostingEnvironment;
		public TripController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(mediator, _httpContextAccessor)
		{
			_hostingEnvironment = environment;
		}

		/// <summary>
		/// Method to get all trips
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetTrips")]
		[ProducesResponseType(typeof(TripResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTrips(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetTripQuery(currentUser) { }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to get trip by Id
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetTripById")]
		[ProducesResponseType(typeof(TripResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTripById(int tripId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetTripByIdQuery(currentUser) { TripId = tripId }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to save trip
		/// </summary>
		/// <param name="tripDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveTrip")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveTrip(TripRequest tripDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertTripCommand() { TripDetail = tripDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		///  Method to delete trip
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("DeleteTrip")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteTrip(int tripId, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new DeleteTripCommand() { TripId = tripId, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Trip;
using TRIP.Platform.Service.Core.Models.Request;

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
		/// Method to save trip
		/// </summary>
		/// <param name="tripDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveTripRequest")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveVehicle(TripRequest tripDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertTripCommand() { TripDetail = tripDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}

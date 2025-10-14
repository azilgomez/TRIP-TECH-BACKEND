using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Trips
{
	public class InsertTripCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public TripRequest TripDetail { get; set; }
	}
	public class InsertTripCommandHandler : IRequestHandler<InsertTripCommand, Response<bool>>
	{
		private readonly ITripService _service;
		public InsertTripCommandHandler(ITripService tripService)
			=> this._service = tripService ?? throw new ArgumentNullException(nameof(tripService));

		/// <summary>
		/// Handler to save engagement details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(InsertTripCommand request, CancellationToken cancellationToken)
		   => await this._service.SaveTrip(request.TripDetail, request.LoggedUser, cancellationToken);
	}
}

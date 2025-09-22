using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Trips
{
	public class DeleteTripCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public int TripId { get; set; }
	}
	public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, Response<bool>>
	{
		private readonly ITripService _service;
		public DeleteTripCommandHandler(ITripService tripService)
			=> this._service = tripService ?? throw new ArgumentNullException(nameof(tripService));

		/// <summary>
		/// Handler to delete trip details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
		   => await this._service.DeleteTrip(request.TripId, request.LoggedUser, cancellationToken);
	}
}

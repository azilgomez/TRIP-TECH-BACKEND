using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Trips
{
	public class GetTripByIdQuery : UserScopedRequest<TripResponse>
	{
		public int TripId { get; set; }
		public GetTripByIdQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripResponse>
	{
		private readonly ITripService _service;
		public GetTripByIdQueryHandler(ITripService tripService)
			=> _service = tripService ?? throw new ArgumentNullException(nameof(tripService));
		public async Task<TripResponse> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
			=> await _service.GetTripById(request.TripId, request.CurrentUserId, cancellationToken);
	}
}

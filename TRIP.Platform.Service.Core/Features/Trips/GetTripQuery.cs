using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Trips
{
	public class GetTripQuery : UserScopedRequest<IEnumerable<TripResponse>>
	{
		public GetTripQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetTripQueryHandler : IRequestHandler<GetTripQuery, IEnumerable<TripResponse>>
	{
		private readonly ITripService _service;
		public GetTripQueryHandler(ITripService tripService)
			=> _service = tripService ?? throw new ArgumentNullException(nameof(tripService));
		public async Task<IEnumerable<TripResponse>> Handle(GetTripQuery request, CancellationToken cancellationToken)
			=> await _service.GetTrips(request.CurrentUserId, cancellationToken);
	}
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vehicles
{
	public class GetVehicleQuery : UserScopedRequest<IEnumerable<VehicleResponse>>
	{
		public GetVehicleQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, IEnumerable<VehicleResponse>>
	{
		private readonly IVehicleService _service;
		public GetVehicleQueryHandler(IVehicleService vehicleService)
			=> _service = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
		public async Task<IEnumerable<VehicleResponse>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
			=> await _service.GetVehicles(request.CurrentUserId, cancellationToken);
	}
}

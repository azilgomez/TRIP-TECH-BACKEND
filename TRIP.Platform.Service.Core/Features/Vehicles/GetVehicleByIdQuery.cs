using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vehicles
{
	public class GetVehicleByIdQuery : UserScopedRequest<VehicleResponse>
	{
		public int VehicleId { get; set; }
		public GetVehicleByIdQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleResponse>
	{
		private readonly IVehicleService _service;
		public GetVehicleByIdQueryHandler(IVehicleService vehicleService)
			=> _service = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
		public async Task<VehicleResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
			=> await _service.GetVehicleById(request.VehicleId, request.CurrentUserId, cancellationToken);
	}
}

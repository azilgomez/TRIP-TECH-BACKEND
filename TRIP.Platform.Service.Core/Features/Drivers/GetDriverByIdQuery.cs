using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Drivers
{
	public class GetDriverByIdQuery : UserScopedRequest<DriverResponse>
	{
		public int DriverId { get; set; }
		public GetDriverByIdQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverResponse>
	{
		private readonly IDriverService _service;
		public GetDriverByIdQueryHandler(IDriverService driverService)
			=> _service = driverService ?? throw new ArgumentNullException(nameof(driverService));
		public async Task<DriverResponse> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
			=> await _service.GetDriverById(request.DriverId, request.CurrentUserId, cancellationToken);
	}
}
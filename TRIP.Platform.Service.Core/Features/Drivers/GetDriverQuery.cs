using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Drivers
{
	public class GetDriverQuery : UserScopedRequest<IEnumerable<DriverResponse>>
	{
		public GetDriverQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, IEnumerable<DriverResponse>>
	{
		private readonly IDriverService _service;
		public GetDriverQueryHandler(IDriverService driverService)
			=> _service = driverService ?? throw new ArgumentNullException(nameof(driverService));
		public async Task<IEnumerable<DriverResponse>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
			=> await _service.GetDrivers(request.CurrentUserId, cancellationToken);
	}
}
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vendors
{
	public class GetVendorQuery : UserScopedRequest<IEnumerable<VendorResponse>>
	{
		public GetVendorQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetDriverQueryHandler : IRequestHandler<GetVendorQuery, IEnumerable<VendorResponse>>
	{
		private readonly IVendorService _service;
		public GetDriverQueryHandler(IVendorService vendorService)
			=> _service = vendorService ?? throw new ArgumentNullException(nameof(vendorService));
		public async Task<IEnumerable<VendorResponse>> Handle(GetVendorQuery request, CancellationToken cancellationToken)
			=> await _service.GetVendors(request.CurrentUserId, cancellationToken);
	}
}

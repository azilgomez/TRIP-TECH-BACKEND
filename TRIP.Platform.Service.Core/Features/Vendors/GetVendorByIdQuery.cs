using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vendors
{
	public class GetVendorByIdQuery : UserScopedRequest<VendorResponse>
	{
		public int VendorId { get; set; }
		public GetVendorByIdQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorResponse>
	{
		private readonly IVendorService _service;
		public GetVendorByIdQueryHandler(IVendorService vendorService)
			=> _service = vendorService ?? throw new ArgumentNullException(nameof(vendorService));
		public async Task<VendorResponse> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
			=> await _service.GetVendorById(request.VendorId, request.CurrentUserId, cancellationToken);
	}
}

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vendors
{
	public class InsertVendorCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public VendorRequest VendorDetail { get; set; }
	}
	public class InsertDriverCommandHandler : IRequestHandler<InsertVendorCommand, Response<bool>>
	{
		private readonly IVendorService _service;
		public InsertDriverCommandHandler(IVendorService vendorService)
			=> this._service = vendorService ?? throw new ArgumentNullException(nameof(vendorService));

		/// <summary>
		/// Handler to save Vendor
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(InsertVendorCommand request, CancellationToken cancellationToken)
		   => await this._service.SaveVendor(request.VendorDetail, request.LoggedUser, cancellationToken);
	}
}

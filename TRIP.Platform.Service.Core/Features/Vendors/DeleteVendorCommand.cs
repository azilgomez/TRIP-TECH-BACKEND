using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vendors
{
	public class DeleteVendorCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public int VendorId { get; set; }
	}
	public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Response<bool>>
	{
		private readonly IVendorService _service;
		public DeleteVendorCommandHandler(IVendorService vendorService)
			=> this._service = vendorService ?? throw new ArgumentNullException(nameof(vendorService));

		/// <summary>
		/// Handler to delete driver details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
		   => await this._service.DeleteVendor(request.VendorId, request.LoggedUser, cancellationToken);
	}
}

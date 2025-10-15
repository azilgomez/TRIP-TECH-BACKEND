using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Features.Vendors;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.API.Controllers
{
	public class VendorController : BaseController
	{
		IWebHostEnvironment _hostingEnvironment;
		public VendorController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(mediator, _httpContextAccessor)
		{
			_hostingEnvironment = environment;
		}

		/// <summary>
		/// Method to all vendor details
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetVendors")]
		[ProducesResponseType(typeof(VendorResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetVendors(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetVendorQuery(currentUser) { }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to get vendor by Id
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetVendorById")]
		[ProducesResponseType(typeof(VendorResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetVendorById(int vendorId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetVendorByIdQuery(currentUser) { VendorId = vendorId }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to save driver
		/// </summary>
		/// <param name="vendorDetail"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("SaveVendor")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> SaveVendor(VendorRequest vendorDetail, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new InsertVendorCommand() { VendorDetail = vendorDetail, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}

		/// <summary>
		/// Method to delete vendor
		/// </summary>
		/// <param name="vendorId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("DeleteVendor")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteVendor(int vendorId, CancellationToken cancellationToken)
		{
			var result = await this._mediator.Send(new DeleteVendorCommand() { VendorId = vendorId, LoggedUser = currentUser }, cancellationToken);
			return this.Ok(result);
		}
	}
}
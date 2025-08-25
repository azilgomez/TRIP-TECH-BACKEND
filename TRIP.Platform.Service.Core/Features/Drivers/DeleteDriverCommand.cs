using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Drivers
{
	public class DeleteDriverCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public int DriverId { get; set; }
	}
	public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Response<bool>>
	{
		private readonly IDriverService _service;
		public DeleteDriverCommandHandler(IDriverService driverService)
			=> this._service = driverService ?? throw new ArgumentNullException(nameof(driverService));

		/// <summary>
		/// Handler to delete driver details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
		   => await this._service.DeleteDriver(request.DriverId, request.LoggedUser, cancellationToken);
	}
}

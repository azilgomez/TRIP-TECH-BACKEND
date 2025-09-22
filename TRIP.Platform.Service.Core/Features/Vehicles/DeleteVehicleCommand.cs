using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vehicles
{
	public class DeleteVehicleCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public int VehicleId { get; set; }
	}
	public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Response<bool>>
	{
		private readonly IVehicleService _service;
		public DeleteVehicleCommandHandler(IVehicleService vehicleService)
			=> this._service = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));

		/// <summary>
		/// Handler to save engagement details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
		   => await this._service.DeleteVehicle(request.VehicleId, request.LoggedUser, cancellationToken);
	}
}

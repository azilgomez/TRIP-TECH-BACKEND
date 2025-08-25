using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Vehicles
{
	public class InsertVehicleCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public VehicleRequest VehicleDetail { get; set; }
	}
	public class InsertVehicleCommandHandler : IRequestHandler<InsertVehicleCommand, Response<bool>>
	{
		private readonly IVehicleService _service;
		public InsertVehicleCommandHandler(IVehicleService vehicleService)
			=> this._service = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));

		/// <summary>
		/// Handler to save engagement details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(InsertVehicleCommand request, CancellationToken cancellationToken)
		   => await this._service.SaveVehicle(request.VehicleDetail, request.LoggedUser, cancellationToken);
	}
}

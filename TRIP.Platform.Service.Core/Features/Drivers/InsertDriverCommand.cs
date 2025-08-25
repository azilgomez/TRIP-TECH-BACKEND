using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Drivers
{
	public class InsertDriverCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public DriverRequest DriverDetail { get; set; }
	}
	public class InsertDriverCommandHandler : IRequestHandler<InsertDriverCommand, Response<bool>>
	{
		private readonly IDriverService _service;
		public InsertDriverCommandHandler(IDriverService driverService)
			=> this._service = driverService ?? throw new ArgumentNullException(nameof(driverService));

		/// <summary>
		/// Handler to save Driver
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(InsertDriverCommand request, CancellationToken cancellationToken)
		   => await this._service.SaveDriver(request.DriverDetail, request.LoggedUser, cancellationToken);
	}
}

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Users
{
	public class InsertUserCommand : IRequest<Response<bool>>
	{
		public string LoggedUser { get; set; }
		public UserRequest UserDetail { get; set; }
	}
	public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, Response<bool>>
	{
		private readonly IUserService _service;
		public InsertUserCommandHandler(IUserService userService)
			=> this._service = userService ?? throw new ArgumentNullException(nameof(userService));

		/// <summary>
		/// Handler to save engagement details.
		/// </summary>
		/// <param name="request">InsertUserCommand request</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>bool</returns>
		public async Task<Response<bool>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
		   => await this._service.SaveUser(request.UserDetail, request.LoggedUser, cancellationToken);
	}
}

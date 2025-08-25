using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Response;
using MediatR;
using TRIP.Platform.Service.Core.Interfaces.Services;

namespace TRIP.Platform.Service.Core.Features.Users
{
	public class GetUsersQuery : UserScopedRequest<IEnumerable<UserResponse>>
	{
		public GetUsersQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
	{
		private readonly IUserService _service;
		public GetUsersQueryHandler(IUserService userService)
	        => this._service = userService ?? throw new ArgumentNullException(nameof(userService));
		public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
			=> await this._service.GetUsers(request.CurrentUserId, cancellationToken);
	}
}

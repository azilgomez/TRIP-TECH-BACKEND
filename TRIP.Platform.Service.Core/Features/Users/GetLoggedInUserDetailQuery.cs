using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Interfaces.Services;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Features.Users
{
	public class GetLoggedInUserDetailQuery : UserScopedRequest<UserResponse>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public GetLoggedInUserDetailQuery(string currentUserId) : base(currentUserId)
		{

		}
	}
	public class GetLoggedInUserDetailQueryHandler : IRequestHandler<GetLoggedInUserDetailQuery, UserResponse>
	{
		private readonly IUserService _service;
		public GetLoggedInUserDetailQueryHandler(IUserService userService)
			=> this._service = userService ?? throw new ArgumentNullException(nameof(userService));
		public async Task<UserResponse> Handle(GetLoggedInUserDetailQuery request, CancellationToken cancellationToken)
			=> await this._service.GetLoggedInUserDetail(request.Email, request.Password, cancellationToken);
	}
}

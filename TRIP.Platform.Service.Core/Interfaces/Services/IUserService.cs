using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Models.Request;
using TRIP.Platform.Service.Core.Models.Response;

namespace TRIP.Platform.Service.Core.Interfaces.Services
{
	public interface IUserService
	{
		Task<IEnumerable<UserResponse>> GetUsers(string loggedUser, CancellationToken cancellationToken);
		Task<UserResponse> GetLoggedInUserDetail(string email, string password, CancellationToken cancellationToken);
		Task<Response<bool>> SaveUser(UserRequest user, string loggedUser, CancellationToken cancellationToken);
	}
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Core.Interfaces.Infrastructure.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		Task<IEnumerable<User>> GetUsers(string loggedUser, CancellationToken cancellationToken);
		Task<User> GetLoggedInUserDetail(string email, string password, CancellationToken cancellationToken);
		Task<bool> SaveUser(User user, string loggedUser, CancellationToken cancellationToken);
	}
}

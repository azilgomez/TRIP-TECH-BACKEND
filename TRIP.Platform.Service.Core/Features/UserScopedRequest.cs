using MediatR;

namespace TRIP.Platform.Service.Core.Features
{
	public abstract class UserScopedRequest<T> : IRequest<T>
	{
		public string CurrentUserId { get; }

		protected UserScopedRequest(string currentUserId)
		{
			this.CurrentUserId = currentUserId;
		}
	}
}
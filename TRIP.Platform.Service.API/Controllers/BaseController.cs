using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TRIP.Platform.Service.API.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
	public class BaseController : Controller
	{
		protected readonly IMediator _mediator;
		protected readonly string currentUser;
		public BaseController()
		{

		}
		public BaseController(IMediator mediator, IHttpContextAccessor _httpContextAccessor)
		{
			this._mediator = mediator;
			string userName = "Liza Rajesh";//_httpContextAccessor.HttpContext.User.Identity.Name ?? _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type.Equals("preferred_username"))?.Value;
			if (!string.IsNullOrEmpty(userName))
			{
				// temporary fix for google external accounts
				this.currentUser = userName.Contains("#") ? userName.Split('#')[1] : userName;
			}

		}
	}
}
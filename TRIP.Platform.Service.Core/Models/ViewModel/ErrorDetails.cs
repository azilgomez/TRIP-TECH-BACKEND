using System.Text.Json;

namespace TRIP.Platform.Service.Core.Models.ViewModel
{
	public class ErrorDetails
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}

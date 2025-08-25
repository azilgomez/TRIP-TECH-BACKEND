using System;

namespace TRIP.Platform.Service.Core.Entities
{
	public class EntityBase
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}

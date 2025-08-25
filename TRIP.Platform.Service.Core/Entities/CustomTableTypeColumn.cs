using System.ComponentModel.DataAnnotations;

namespace TRIP.Platform.Service.Core.Entities
{
	public class CustomTableTypeColumn
    {
        [Key]
        public string COLUMN_NAME { get; set; }
        public string TYPE_NAME { get; set; }
        public bool NULLABLE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EY.CTP.SRED.Platform.Service.Core.Entities
{
    public class StoredProcParam
    {
		[Key]
		public string PARAMETER_NAME { get; set; }
		public string PARAMETER_MODE { get; set; }
		public string DATA_TYPE { get; set; }
		public string USER_DEFINED_TYPE_SCHEMA { get; set; }
		public string USER_DEFINED_TYPE_NAME { get; set; }
	}
}

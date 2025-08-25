using System.Collections.Generic;

namespace TRIP.Platform.Service.Core.Models.Response
{
    public class Response<T>
    {
        /// <summary>
        /// Get or set IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }
        public int InsertCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
        public bool HasMoreRecords { get; set; }

        /// <summary>
        /// Get or set data list if it is a collection of objects
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Get or set data item if it is a single object.
        /// </summary>
        public T DataItem { get; set; }

        /// <summary>
        /// Get or set Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set ValidationMessages
        /// </summary>
        public List<string> ValidationMessages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models
{
    public class CustomError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public IList<KeyValuePair<string, IList<string>>> ErrorDetails { get; set; }
        public Exception ExceptionObject { get; set; }

    }
}

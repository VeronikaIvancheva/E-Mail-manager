using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailManager.Data.Utilities
{
    public class Logger : ILogger
    {
        [DataType(DataType.DateTime)]
        public DateTime ReceiveDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public DateTime CurrentStatus { get; set; }
        public DateTime TerminalStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Entities
{
    public class DiscordLog
    {
        public string LogMessage { get; set; }
        public double CreateDate { get; set; }
        public string LogSeverity { get; set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
    }
}

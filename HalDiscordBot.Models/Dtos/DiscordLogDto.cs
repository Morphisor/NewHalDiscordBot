using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Dtos
{
    public class DiscordLogDto
    {
        public string LogMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public LogSeverity LogSeverity { get; set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
    }
}

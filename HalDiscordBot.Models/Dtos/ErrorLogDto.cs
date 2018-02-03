using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Dtos
{
    public class ErrorLogDto
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

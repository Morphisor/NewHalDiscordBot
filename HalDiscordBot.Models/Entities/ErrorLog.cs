﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Entities
{
    public class ErrorLog
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public double CreateDate { get; set; }
    }
}

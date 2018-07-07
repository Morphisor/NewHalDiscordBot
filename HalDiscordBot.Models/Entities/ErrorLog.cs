using Morphisor.SQLiteORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Entities
{
    public class ErrorLog
    {
        [SQLitePrimaryKey]
        public int ErrorLogId { get; set; }

        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public double CreateDate { get; set; }
    }
}

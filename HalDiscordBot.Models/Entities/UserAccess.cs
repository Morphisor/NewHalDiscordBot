using HalDiscrodBot.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Entities
{
    public class UserAccess
    {
        [SQLitePrimaryKey]
        public int UserAccessId { get; set; }

        public string UserName { get; set; }
        public double EnterDate { get; set; }
        public double ExitDate { get; set; }
        public string ServerName { get; set; }
    }
}

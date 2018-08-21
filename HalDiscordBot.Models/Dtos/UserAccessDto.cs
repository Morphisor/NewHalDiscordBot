using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Dtos
{
    public class UserAccessDto
    {
        public string UserName { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }
        public string ServerName { get; set; }
    }
}

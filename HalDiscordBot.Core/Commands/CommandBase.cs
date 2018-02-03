using Discord.Commands;
using HalDiscordBot.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Core.Commands
{
    public class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected SQLiteLogger _SQLiteLogger;

        public CommandBase()
        {
            _SQLiteLogger = SQLiteLogger.Instance;
        }
    }
}

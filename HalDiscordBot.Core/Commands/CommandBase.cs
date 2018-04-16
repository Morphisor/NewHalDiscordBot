using Discord.Commands;
using HalDiscordBot.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.Commands
{
    public class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected SQLiteLogger _SQLiteLogger;

        public CommandBase()
        {
            _SQLiteLogger = SQLiteLogger.Instance;
        }

        protected async Task HandleError(string message, Exception ex)
        {
            await Context.Channel.SendMessageAsync("Something went wrong!");
            _SQLiteLogger.LogError(message, ex.Message, ex.StackTrace);
        }
    }
}

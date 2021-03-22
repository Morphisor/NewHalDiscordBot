using Discord.Commands;
using HalDiscordBot.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.Commands
{
    public abstract class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected ConsoleLogger _consoleLogger;

        public CommandBase()
        {
            _consoleLogger = ConsoleLogger.Instance;
        }

        protected async Task HandleError(string message, Exception ex)
        {
            await Context.Channel.SendMessageAsync("Something went wrong!");
            _consoleLogger.Log(message, ex);
        }
    }
}

using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HalDiscordBot.Commands.TestCommands
{
    public class TestCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Echos a message.")]
        public async Task Ping([Remainder] [Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }
    }
}

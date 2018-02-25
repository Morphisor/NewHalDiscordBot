using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Discord;

namespace HalDiscordBot.Core.Commands
{
    public class AdministrationCommands : CommandBase
    {
        [Command("prune")]
        [Summary("Removes the last messages of the specified user")]
        public async Task Prune([Remainder] [Summary("The user to prune")] string userName)
        {
            string userToPrune = !string.IsNullOrEmpty(userName) ? userName : "HAL";
            var messages = await Context.Channel.GetMessagesAsync(20).ToList();
            var transormed = messages.SelectMany(msg => msg);
            var toDelete = transormed.Where(msg => msg.Author.Username == userToPrune);
            await Context.Channel.DeleteMessagesAsync(toDelete);
        }
    }
}

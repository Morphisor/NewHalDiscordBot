using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Discord;
using Discord.WebSocket;

namespace HalDiscordBot.Core.Commands
{
    public class AdministrationCommands : CommandBase
    {
        [Command("prune")]
        [Summary("Removes the last messages of the specified user")]
        public async Task Prune([Remainder] [Summary("The user to prune")] string userName)
        {
            string userToPrune = !string.IsNullOrEmpty(userName) ? userName : "HAL";
            var messages = await Context.Channel.GetMessagesAsync(20).ToListAsync();
            var transormed = messages.SelectMany(msg => msg);
            var toDelete = transormed.Where(msg => msg.Author.Username == userToPrune);
            var tasks = new List<Task>();
            foreach (var item in toDelete)
            {
                tasks.Add(Context.Channel.DeleteMessageAsync(item));
            }
            Task.WaitAll(tasks.ToArray());
        }

        [Command("color")]
        [Summary("Assign the specified color to the user")]
        public async Task AssignColor([Remainder] [Summary("The hex color")] string colorCode)
        {
            colorCode = colorCode.Replace("#", "");
            int converted = Convert.ToInt32(colorCode, 16);
            System.Drawing.Color convertedColor = System.Drawing.Color.FromArgb(converted);
            Color actualColor = new Color(convertedColor.R, convertedColor.G, convertedColor.B);

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.Equals(Context.User.Username + "-Color"));

            if (role != null && !role.Color.Equals(actualColor))
            {
                await role.ModifyAsync((prop) => { prop.Color = actualColor; });
                await Context.Channel.SendMessageAsync("Updated color");

                var castedUser = Context.User as SocketGuildUser;
                if (!castedUser.Roles.Any(rl => rl.Name.Equals(Context.User.Username + "-Color") ))
                {
                    try
                    {
                        await castedUser.AddRoleAsync(role);
                        await Context.Channel.SendMessageAsync("Assigned color");
                    }
                    catch (Exception ex)
                    {
                        await HandleError("Error updating color", ex);
                    }
                }
            }
            else if (role == null)
            {
                var newRole = await Context.Guild.CreateRoleAsync(Context.User.Username + "-Color", null, actualColor, false, null);
                var castedUser = Context.User as SocketGuildUser;
                try
                {
                    await castedUser.AddRoleAsync(newRole);
                    await Context.Channel.SendMessageAsync("Color created and updated");
                }
                catch (Exception ex)
                {
                    await HandleError("Error creating color", ex);
                }
            }

        }

        [Command("k")]
        [Summary("Kicks the specyfied user")]
        public async Task Kick([Remainder] [Summary("The user to kick")] string userName)
        {
            var user = Context.Guild.Users.FirstOrDefault(usr => usr.Username == userName);

            if (user == null || (user != null && user.VoiceChannel == null))
            {
                await Context.Channel.SendMessageAsync("User not found or not in a voice channel!");
                return;
            }

            try
            {
                var kickChannel = await Context.Guild.CreateVoiceChannelAsync("KickChannel");
                await user.ModifyAsync((prop) => prop.Channel = kickChannel);
                await kickChannel.DeleteAsync();
                await Context.Channel.SendMessageAsync($"Kicked user {userName}");
            }
            catch (Exception ex)
            {
                await HandleError("Error kicking user", ex);
            }
        }
    }
}

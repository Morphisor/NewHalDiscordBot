using Discord.WebSocket;
using HalDiscordBot.Core.CustomLogic.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.CustomLogic.UserUpdated
{
    public class HalUserUpdated : UserUpdatedLogic
    {
        public HalUserUpdated(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public override void UserJoined(string user)
        {
            RemoveLog(user);
        }

        public override void UserLeft(string user)
        {
            RemoveLog(user);
        }

        public override void UserMoved(string userName, string from, string to)
        {
            if (from != "KickChannel" && to != "KickChannel")
                RemoveLog(userName);
        }

        private void RemoveLog(string userName)
        {
            if(userName == "HAL")
            {
                var message = _currentChannel.GetMessagesAsync(2).ToListAsync();
                var transformed = message.GetAwaiter().GetResult().SelectMany(msg => msg);
                var tasks = new List<Task>();
                foreach (var item in transformed)
                {
                    if (item.Content.Contains("joined.") || item.Content.Contains("left."))
                    tasks.Add(_currentChannel.DeleteMessageAsync(item));
                }
                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}

using Discord.WebSocket;
using HalDiscordBot.Core.CustomLogic.Bases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HalDiscordBot.Core.CustomLogic.UserUpdated
{
    public class FedeUserUpdated : UserUpdatedLogic
    {
        public FedeUserUpdated(ISocketMessageChannel currentChannel) : base(currentChannel)
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
            if(userName == "Federik")
            {
                var message = _currentChannel.GetMessagesAsync(1).ToList();
                var transformed = message.GetAwaiter().GetResult().SelectMany(msg => msg);
                var task = _currentChannel.DeleteMessagesAsync(transformed);
                task.GetAwaiter().GetResult();
            }
        }
    }
}

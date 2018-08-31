using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;

namespace HalDiscordBot.Core.CustomLogic.Bases
{
    public abstract class UserUpdatedLogic : BaseCustomLogic
    {
        public UserUpdatedLogic(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public abstract void UserMoved(string userName, string from, string to);
        public abstract void UserJoined(string user);
        public abstract void UserLeft(string user);
    }
}

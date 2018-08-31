using Discord.WebSocket;
using HalDiscordBot.Core.CustomLogic.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Core.CustomLogic.UserUpdated
{
    public class TestUserUpdated : UserUpdatedLogic
    {
        public TestUserUpdated(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public override void UserJoined(string user)
        {
            
        }

        public override void UserLeft(string user)
        {
            
        }

        public override void UserMoved(string userName, string from, string to)
        {
            
        }
    }
}

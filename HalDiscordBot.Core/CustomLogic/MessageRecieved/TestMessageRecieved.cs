using Discord.WebSocket;
using HalDiscordBot.Core.CustomLogic.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Core.CustomLogic.MessageRecieved
{
    public class TestMessageRecieved : MessageRecievedLogic
    {
        public TestMessageRecieved(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public override void OnMessageRecieved(SocketMessage message)
        {
            
        }
    }
}

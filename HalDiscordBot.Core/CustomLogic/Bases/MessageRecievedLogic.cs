using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Core.CustomLogic.Bases
{
    public abstract class MessageRecievedLogic : BaseCustomLogic
    {
        public MessageRecievedLogic(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public abstract void OnMessageRecieved(SocketMessage message);
    }
}

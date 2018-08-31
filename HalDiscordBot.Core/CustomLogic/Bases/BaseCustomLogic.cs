using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Core.CustomLogic.Bases
{
    public abstract class BaseCustomLogic
    {
        protected readonly ISocketMessageChannel _currentChannel;

        public BaseCustomLogic(ISocketMessageChannel currentChannel)
        {
            _currentChannel = currentChannel;
        }
    }
}

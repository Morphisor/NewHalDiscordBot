using Discord.WebSocket;
using HalDiscordBot.Core.CustomLogic.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace HalDiscordBot.Core.CustomLogic.UserUpdated
{
    public class OkoropUserUpdated : UserUpdatedLogic
    {
        private SocketGuildUser _userToUnmuteRef;

        public OkoropUserUpdated(ISocketMessageChannel currentChannel) : base(currentChannel)
        {
        }

        public override void UserJoined(string user)
        {
            if (user == "okorop")
            {
                var users = _currentChannel.GetUsersAsync().ToList().GetAwaiter().GetResult().SelectMany(u => u);
                var desiredUser = users.First(u => u.Username == user) as SocketGuildUser;
                desiredUser.ModifyAsync((prop) => { prop.Mute = true; });
                _userToUnmuteRef = desiredUser;
                var unmuteTimer = new Timer();
                unmuteTimer.Elapsed += UnmuteTimer_Elapsed;
                unmuteTimer.Interval = 10000;
                unmuteTimer.Enabled = true;
            }
        }

        public override void UserLeft(string user)
        {
            
        }

        public override void UserMoved(string userName, string from, string to)
        {
            
        }

        private void UnmuteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _userToUnmuteRef.ModifyAsync((prop) => { prop.Mute = false; });
        }
    }
}

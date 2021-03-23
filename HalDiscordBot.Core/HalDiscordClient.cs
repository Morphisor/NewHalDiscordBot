using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HalDiscordBot.Log;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HalDiscrodBot.Utils;
using HalDiscordBot.Core.Config;
using System.Linq;
using HalDiscordBot.Core.CustomLogic;

namespace HalDiscordBot.Core
{
    public class HalDiscordClient
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        private ConsoleLogger _consoleLogger;
        private ConfigurationService _configService;

        public HalDiscordClient()
        {
            _consoleLogger = ConsoleLogger.Instance;
            _configService = ConfigurationService.Instance;
        }

        public async Task Connect()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _client.Log += Log;

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = _configService.Config.Token;

            _client.MessageReceived += MessageRecieved;
            _client.UserVoiceStateUpdated += UserVoiceStateUpdated;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                await _commands.AddModulesAsync(assembly, _services);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState before, SocketVoiceState after)
        {
            var userCasted = user as SocketGuildUser;
            var guild = userCasted.Guild;
            var mainChannel = guild.TextChannels.First(cnl => cnl.Name == _configService.Config.MainChannelName);

            if (before.VoiceChannel != null && after.VoiceChannel != null && before.VoiceChannel.Name != after.VoiceChannel.Name && after.VoiceChannel.Name != "KickChannel")
            {
                await mainChannel.SendMessageAsync($"User {user.Username} moved from {before.VoiceChannel.Name} to {after.VoiceChannel.Name}.");
                LogicExecutor.Exec(LogicType.UserUpdated, "UserMoved", new object[] { user.Username, before.VoiceChannel.Name, after.VoiceChannel.Name }, mainChannel);
            }
            else if (before.VoiceChannel != null && after.VoiceChannel == null)
            {
                await mainChannel.SendMessageAsync($"User {user.Username} left.");
                LogicExecutor.Exec(LogicType.UserUpdated, "UserLeft", new object[] { user.Username }, mainChannel);
            }
            else if (before.VoiceChannel == null && after.VoiceChannel != null)
            {
                await mainChannel.SendMessageAsync($"User {user.Username} joined.");
                LogicExecutor.Exec(LogicType.UserUpdated, "UserJoined", new object[] { user.Username }, mainChannel);
            }
        }

        private async Task MessageRecieved(SocketMessage message)
        {
            var msg = message as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (!(msg.HasCharPrefix('$', ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;

            var context = new SocketCommandContext(_client, msg);
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);

            LogicExecutor.Exec(LogicType.MessageRecieved, "OnMessageRecieved", new object[] { message }, context.Channel);
        }

        private Task Log(LogMessage message)
        {
            _consoleLogger.Log(message.ToString());
            return Task.CompletedTask;
        }

    }
}

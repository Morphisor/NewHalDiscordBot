using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.Commands
{
    public class AudioCommands : CommandBase
    {
        [Command("a", RunMode = RunMode.Async)]
        [Summary("Play the specified audio file")]
        public async Task PlayAudio([Remainder] [Summary("Name of the file to play")] string fileName)
        {
            var user = Context.Guild.Users.FirstOrDefault(usr => usr.Username == Context.User.Username);
            SocketVoiceChannel voiceChannel = null;

            if (user == null || (user != null && user.VoiceChannel == null))
            {
                var maxUsers = Context.Guild.VoiceChannels.Max(vc => vc.Users.Count);
                voiceChannel = Context.Guild.VoiceChannels.First(vc => vc.Users.Count == maxUsers);
            }
            else
            {
                voiceChannel = user.VoiceChannel;
            }

            var audioClient = await voiceChannel.ConnectAsync();

            try
            {
                var ffmpeg = CreateStream(fileName);
                var output = ffmpeg.StandardOutput.BaseStream;
                var discord = audioClient.CreatePCMStream(AudioApplication.Mixed);
                await output.CopyToAsync(discord);
                await discord.FlushAsync();
            }
            catch (Exception e)
            {
                await HandleError("Something went wrong", e);
            }
            finally
            {
                await audioClient.StopAsync();
            }
        }

        [Command("alist")]
        [Summary("List of avaiable audio files")]
        public async Task ListAudioFiles()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "AudioFiles");
            var files = Directory.EnumerateFiles(path, "*.mp3", SearchOption.TopDirectoryOnly);
            var result = string.Empty;

            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    result += Path.GetFileName(file) + "\n";
                }
            }

            await ReplyAsync(result);
        }

        private Process CreateStream(string fileName)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "AudioFiles", fileName);
            var ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i {path} -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            return Process.Start(ffmpeg);
        }
    }
}

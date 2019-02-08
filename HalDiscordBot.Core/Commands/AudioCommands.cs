using Discord.Audio;
using Discord.Commands;
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
            if (user == null || (user != null && user.VoiceChannel == null))
            {
                await Context.Channel.SendMessageAsync("User not in a voice channel!");
                return;
            }

            var audioClient = await user.VoiceChannel.ConnectAsync();
            var ffmpeg = CreateStream(fileName);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = audioClient.CreatePCMStream(AudioApplication.Mixed);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();
            await audioClient.StopAsync();
        }

        [Command("alist")]
        [Summary("List of avaiable audio files")]
        public async Task ListAudioFiles()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "AudioFiles");
            var files = Directory.EnumerateFiles(path, "*.mp3", SearchOption.TopDirectoryOnly);
            var result = string.Empty;

            if(files.Count() > 0)
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

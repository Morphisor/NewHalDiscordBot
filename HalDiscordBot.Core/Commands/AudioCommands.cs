using Discord;
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
        public async Task PlayAudio([Remainder][Summary("Name of the file to play")] string fileName)
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

        [Command("r", RunMode = RunMode.Async)]
        [Summary("Record channel audio")]
        public async Task RecordAudio([Remainder][Summary("The username of the user")] string userName)
        {
            var user = Context.Guild.Users.FirstOrDefault(usr => usr.DisplayName == userName);

            var voiceChannel = user.VoiceChannel;
            var audioClient = await voiceChannel.ConnectAsync();

            var path = Path.Combine(Environment.CurrentDirectory, "Recording.m4a");
            if (File.Exists(path)) File.Delete(path);


            using (var ffmpeg = CreateFfmpegOut(path))
            {
                using (var ffmpegOutStdinStream = ffmpeg.StandardInput.BaseStream)
                {
                    try
                    {
                        var buffer = new byte[3840];

                        var stopwatch = new Stopwatch();
                        stopwatch.Start();

                        while (stopwatch.ElapsedMilliseconds < 15000)
                        {
                            await user.AudioStream.ReadAsync(buffer, 0, buffer.Length);
                            await ffmpegOutStdinStream.WriteAsync(buffer, 0, buffer.Length);
                            await ffmpegOutStdinStream.FlushAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        _consoleLogger.Log($"Error while recording, {e.Message}");
                    }
                    finally
                    {
                        await ffmpegOutStdinStream.FlushAsync();
                        ffmpegOutStdinStream.Close();
                        ffmpeg.Close();
                        await voiceChannel.DisconnectAsync();
                    }
                }
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

        private Process CreateFfmpegOut(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -ac 2 -f s16le -ar 48000 -i pipe:0 -acodec pcm_u8 -ar 22050 -f wav \"{path}\"",
                UseShellExecute = false,
                RedirectStandardInput = true,
            });
        }
    }
}

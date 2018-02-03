using HalDiscordBot.Core;
using System;
using System.Threading.Tasks;

namespace HalDiscrodBot.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var client = new HalDiscordClient();
            await client.Connect();
        }
    }
}

using Discord.Commands;
using HalDiscordBot.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.Commands
{
    public class NSFWCommands : CommandBase
    {
        [Command("boob")]
        [Summary("Random boobs")]
        public async Task Boob()
        {
            var rng = new Random();
            var restService = new RestService("http://api.oboobs.ru/boobs");
            try
            {
                var response = await restService.MakeRequest($"/{rng.Next(10330)}");
                JToken responseParsed = JToken.Parse(response);
                var image = responseParsed[0]["preview"];
                await Context.Channel.SendMessageAsync($"http://media.oboobs.ru/{image}");
            }
            catch (Exception ex)
            {
                await HandleError("Error retrieving boobs", ex);
            }
        }

        [Command("ass")]
        [Summary("Random asses")]
        public async Task Ass()
        {
            var rng = new Random();
            var restService = new RestService("http://api.obutts.ru/butts");
            try
            {
                var response = await restService.MakeRequest($"/{rng.Next(4335)}");
                JToken responseParsed = JToken.Parse(response);
                var image = responseParsed[0]["preview"];
                await Context.Channel.SendMessageAsync($"http://media.obutts.ru/{image}");
            }
            catch (Exception ex)
            {
                await HandleError("Error retrieving Asses", ex);
            }
        }
    }
}

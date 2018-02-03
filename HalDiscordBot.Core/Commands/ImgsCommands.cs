using Discord.Commands;
using HalDiscordBot.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HalDiscordBot.Core.Commands
{
    public class ImgsCommands: CommandBase
    {
        [Command("i")]
        [Summary("Search images through google")]
        public async Task GoogleImg([Remainder] [Summary("The image to search")] string image)
        {
            RestService restService = new RestService("https://www.googleapis.com/customsearch/v1", HttpVerb.GET);
            Random rng = new Random();
            string parsedImgText = image.Replace(" ", "+");
            var response = restService.MakeRequest($"?searchType=image&" +
                $"key=AIzaSyAOEXw6WL0-zJuQaiJkVBQ8N3F3-rrV8gM&cx=010655637071118084004:bf9d6vyljnw&" +
                $"q={parsedImgText}&start={rng.Next(1, 100)}&num=1");

            string link = null;
            JObject result = JObject.Parse(await response);
            if (result["items"] != null)
            {
                JToken item = result["items"][0];
                link = item["link"].ToString();
            }

            if (link == null)
            {
                await ReplyAsync("Immagine no trovata");
                return;
            }

            await ReplyAsync(link);
        }
    }
}

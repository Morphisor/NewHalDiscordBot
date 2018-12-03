using Discord.Commands;
using HalDiscordBot.Rest;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HalDiscrodBot.Utils;

namespace HalDiscordBot.Core.Commands
{
    public class ImgsCommands : CommandBase
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

        [Command("memelist")]
        [Summary("List of avaiable memes")]
        public async Task MemeList()
        {
            RestService restService = new RestService("https://memegen.link/api/templates/", HttpVerb.GET);
            var response = await restService.MakeRequest();
            var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            var memes = parsed.Select(entry => Path.GetFileName(entry.Value));
            await Context.Channel.SendTableAsync(memes, x => $"{x,-17}", 3);
        }

        [Command("memegen")]
        [Summary("Generate meme")]
        public async Task MemeGen([Summary("meme type")] string meme, [Summary("top text")] string top, [Remainder][Summary("bottom text")]string bottom)
        {
            var url = $"http://memegen.link/{meme}/{top}/{bottom}.jpg";
            await Context.Channel.SendMessageAsync(url);

        }

        [Command("gif")]
        [Summary("Random gif")]
        public async Task Gif([Remainder][Summary("search term")] string search)
        {
            RestService restService = new RestService("http://api.giphy.com/v1/gifs/search", HttpVerb.GET);
            restService.ContentType = "application/json";
            string parsedText = search.Replace(" ", "+");
            var response = restService.MakeRequest($"?api_key=F6lSsIC0UPOhKTkvkwwv3M7wUW09VRV1&q={parsedText}");
            var result = JObject.Parse(await response);
            var data = result["data"] as JArray;
            var resultMessage = "Gif non trovata";
            if (data != null)
            {
                var rng = new Random();
                JToken item = data[rng.Next(0, data.Count - 1)];
                resultMessage = item["embed_url"].Value<string>();
            }

            await ReplyAsync(resultMessage);
        }
    }
}

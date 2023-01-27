using Discord.Commands;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.GPT3.ObjectModels;
using HalDiscordBot.Core.Config;

namespace HalDiscordBot.Core.Commands
{
    public class AICommands : CommandBase
    {
        [Command("genimg")]
        [Summary("Use open ai to generate an image")]
        public async Task Image([Remainder][Summary("The image asked")] string question)
        {
            try
            {
                var aiClient = new OpenAIService(new OpenAiOptions { ApiKey = ConfigurationService.Instance.Config.OpenAIToken });
                var imageResult = await aiClient.Image.CreateImage(new ImageCreateRequest
                {
                    Prompt = question,
                    N = 1,
                    Size = StaticValues.ImageStatics.Size.Size512,
                    ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                    User = "WaC"
                });

                if (imageResult.Successful)
                    await Context.Channel.SendMessageAsync(imageResult.Results.First().Url);
                else
                    await Context.Channel.SendMessageAsync($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
            catch (Exception ex)
            {
                await HandleError("Error generating image", ex);
            }
        } 
    }
}

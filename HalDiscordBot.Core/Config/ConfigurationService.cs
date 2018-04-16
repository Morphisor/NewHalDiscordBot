using HalDiscordBot.Models.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HalDiscordBot.Core.Config
{
    internal class ConfigurationService
    {
        private static ConfigurationService _instance;

        internal static ConfigurationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConfigurationService();

                return _instance;
            }
        }

        public Configuration Config { get; private set; }
        
        private ConfigurationService()
        {
            var configPath = Environment.CurrentDirectory + "\\Config\\config.json"; 
            string configString = File.ReadAllText(configPath);
            Config = JsonConvert.DeserializeObject<Configuration>(configString);
        }


    }
}

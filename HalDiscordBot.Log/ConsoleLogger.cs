using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Log
{
    public class ConsoleLogger
    {
        private static ConsoleLogger _instance;

        public static ConsoleLogger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConsoleLogger();

                return _instance;
            }
        }


        private ConsoleLogger() { }

        public void Log(string msg, Exception ex = null)
        {
            Console.WriteLine(msg);
        }
    }
}

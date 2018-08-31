using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Discord.WebSocket;

namespace HalDiscordBot.Core.CustomLogic
{
    public class LogicExecutor
    {
        public static void Exec(LogicType type, string methodName, object[] args, ISocketMessageChannel currentChannel)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var logics = types.Where(tp => tp.IsClass && tp.Namespace == $"HalDiscordBot.Core.CustomLogic.{type.ToString()}");
            foreach (var logic in logics)
            {
                var ctor = logic.GetConstructor(new Type[] { typeof(ISocketMessageChannel) });
                var instance = ctor.Invoke(new object[] { currentChannel });
                var methodInfo = logic.GetMethod(methodName);
                methodInfo.Invoke(instance, args);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Models.Misc
{
    public class OnErrorArgs<T> : EventArgs 
    {
        public T Model { get; set; }
        public Exception Error { get; set; }

        public OnErrorArgs() { }
        public OnErrorArgs(T model, Exception error)
        {
            Model = model;
            Error = error;
        }
    }
}

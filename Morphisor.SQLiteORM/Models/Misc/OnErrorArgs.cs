using System;
using System.Collections.Generic;
using System.Text;

namespace Morphisor.SQLiteORM.Models.Misc
{
    public class OnErrorArgs<T> : EventArgs 
    {
        public T Model { get; set; }
        public Exception Error { get; set; }
        public OnErrorType Type { get; set; }

        public OnErrorArgs() { }
        public OnErrorArgs(T model, Exception error, OnErrorType type)
        {
            Model = model;
            Error = error;
            Type = type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Morphisor.SQLiteORM.Models.Misc
{
    public class OnPreSaveArgs<T> : EventArgs
    {
        public T Model { get; set; }

        public OnPreSaveArgs() { }
        public OnPreSaveArgs(T model)
        {
            Model = model;
        }
    }
}

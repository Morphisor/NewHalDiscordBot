using Morphisor.SQLiteORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Morphisor.SQLiteORM.Extensions
{
    public static class MiscExtensions
    {
        public static void RemoveLastChar(this StringBuilder builder)
        {
            builder.Length--;
        }

        public static string GetSQLiteType(this PropertyInfo prop)
        {
            var pKeyAttribute = prop.GetCustomAttribute<SQLitePrimaryKey>();

            if (pKeyAttribute != null && prop.PropertyType != typeof(int))
                throw new Exception("The primary key property must be of type int!");
            else if (pKeyAttribute != null)
                return "INTEGER PRIMARY KEY AUTOINCREMENT";


            string toReturn = null;
            if (prop.PropertyType == typeof(int))
                toReturn = "INTEGER";
            else if (prop.PropertyType == typeof(DateTime))
                toReturn = "INTEGER";
            else if (prop.PropertyType == typeof(bool))
                toReturn = "INTEGER";
            else
                toReturn = "TEXT";

            return toReturn;
        }
    }
}

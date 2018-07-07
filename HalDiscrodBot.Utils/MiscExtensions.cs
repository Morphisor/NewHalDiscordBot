using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using HalDiscrodBot.Utils.Attributes;

namespace HalDiscrodBot.Utils
{
    public static class MiscExtensions
    {
        public static double GetUnixTime(this DateTime date)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(date) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static DateTime GetDateTime(this double unixDate)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            date = date.AddSeconds(unixDate).ToLocalTime();
            return date;
        }

        public static string Sanitize(this string stringToClean)
        {
            return Regex.Replace(stringToClean, "[^0-9a-zA-Z]+", "");
        }

        public static bool IsError(this LogMessage message)
        {
            return message.Severity == LogSeverity.Critical ||
                      message.Severity == LogSeverity.Error ||
                      message.Severity == LogSeverity.Warning;
        }

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

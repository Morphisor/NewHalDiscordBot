using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

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

        public static Task SendTableAsync<T>(this IMessageChannel ch, string seed, IEnumerable<T> items, Func<T, string> howToPrint, int columns = 3)
        {
            var i = 0;
            return ch.SendMessageAsync($@"{seed}```css
{string.Join("\n", items.GroupBy(item => (i++) / columns).Select(ig => string.Concat(ig.Select(el => howToPrint(el)))))}
```");
        }

        public static Task SendTableAsync<T>(this IMessageChannel ch, IEnumerable<T> items, Func<T, string> howToPrint, int columns = 3) =>
            ch.SendTableAsync("", items, howToPrint, columns);
    }
}

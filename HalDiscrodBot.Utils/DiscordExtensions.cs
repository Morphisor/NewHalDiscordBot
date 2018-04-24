using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace HalDiscrodBot.Utils
{
    public static class DiscordExtensions
    {
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

using System.Linq;
using System;
using System.Collections.Generic;

namespace Marosoft.Mist.Repl
{
    static class ConsoleExtensions
    {
        public static void WithErrorColors(Action block)
        {
            WithColors(ConsoleColor.White, ConsoleColor.Red, block);
        }

        public static void WithColors(ConsoleColor fg, ConsoleColor bg, Action block)
        {
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
                block.Invoke();
            }
            finally
            {
                Console.ForegroundColor = oldFg;
                Console.BackgroundColor = oldBg;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD2.ConsoleUtils
{
    public static class ColorConsole
    {
        public static void WriteLine(string message, ConsoleColor fcolor)
        {
            WriteLine(message, fcolor, Console.BackgroundColor);
        }

        public static void WriteLine(string message, ConsoleColor fcolor, ConsoleColor bcolor)
        {
            Console.ForegroundColor = fcolor;
            Console.BackgroundColor = bcolor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Write(string message, ConsoleColor fcolor)
        {
            Write(message, fcolor, Console.BackgroundColor);
        }

        public static void Write(string message, ConsoleColor fcolor, ConsoleColor bcolor)
        {
            Console.ForegroundColor = fcolor;
            Console.BackgroundColor = bcolor;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}

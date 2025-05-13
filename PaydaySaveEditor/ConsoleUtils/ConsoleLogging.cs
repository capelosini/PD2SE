using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PD2.ConsoleUtils
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }

    public static class ConsoleLogging
    {
        private static ConsoleColor GetColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    return ConsoleColor.Blue;
                case LogLevel.Warning: 
                    return ConsoleColor.Yellow;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Debug:
                    return ConsoleColor.Gray;
                default:
                    return ConsoleColor.White;
            }
        }

        public static void ShowTitle()
        {
            ColorConsole.WriteLine("______               _               _____   _____                   _____    _ _ _             \n| ___ \\             | |             / __  \\ /  ___|                 |  ___|  | (_) |            \n| |_/ /_ _ _   _  __| | __ _ _   _  `' / /' \\ `--.  __ ___   _____  | |__  __| |_| |_ ___  _ __ \n|  __/ _` | | | |/ _` |/ _` | | | |   / /    `--. \\/ _` \\ \\ / / _ \\ |  __|/ _` | | __/ _ \\| '__|\n| | | (_| | |_| | (_| | (_| | |_| | ./ /___ /\\__/ / (_| |\\ V /  __/ | |__| (_| | | || (_) | |   \n\\_|  \\__,_|\\__, |\\__,_|\\__,_|\\__, | \\_____/ \\____/ \\__,_| \\_/ \\___| \\____/\\__,_|_|\\__\\___/|_|   \n            __/ |             __/ |                                                             \n           |___/             |___/                                                              \n", 
                ConsoleColor.Cyan);
            Log($"V{typeof(Program).Assembly.GetName().Version}\n", LogLevel.Info);
        }

        public static void Log(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public static void Log(string message, LogLevel logLevel)
        {
            ColorConsole.Write($"[{Enum.GetName(typeof(LogLevel), logLevel)}]", GetColor(logLevel));
            Console.WriteLine(" " + message);
        }
    }
}

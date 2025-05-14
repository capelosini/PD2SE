using PD2.GameSave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PD2.ConsoleUtils
{
    public static class DictionaryEditor
    {
        private const int PAGE_SIZE = 15;
        private const bool AUTO_PAGE_SWITCH = true;

        public static void Open(Dictionary<Object, Object> dict, string keyName="GameData")
        {
            Console.Clear();
            int selectedIndex = 0;
            string selectedMark = "\u001b[32m> ";
            string resetColor = "\u001b[0m";
            Object[] keys = dict.Keys.ToArray();
            int maxPages = (int)Math.Ceiling((double)keys.Length / PAGE_SIZE);
            int currentPage = 0;

            bool loop = true;
            while (loop)
            {
                Console.Clear();

                ConsoleLogging.Log($"Editing [{keyName}] | Arrow Keys to navigate, Enter to Select | ESC to go back", LogLevel.Info);

                if (AUTO_PAGE_SWITCH)
                    currentPage = selectedIndex / PAGE_SIZE;

                for (int i=PAGE_SIZE*currentPage; i<Math.Min(PAGE_SIZE * (currentPage+1), keys.Length); i++)
                {
                    Object key = keys[i];
                    Console.WriteLine($"{(i == selectedIndex ? selectedMark : "  ")}{key.ToString()} = {(dict[key] != null ? dict[key].ToString() : "null")}{resetColor}");
                }

                Console.WriteLine($"Page {currentPage + 1}/{maxPages}");

                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = dict.Keys.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        selectedIndex %= dict.Keys.Count;
                        break;
                    case ConsoleKey.RightArrow:
                        currentPage++;
                        currentPage %= maxPages;
                        break;
                    case ConsoleKey.LeftArrow:
                        currentPage--;
                        if (currentPage < 0)
                            currentPage = maxPages - 1;
                        break;
                    case ConsoleKey.Enter:
                        if (dict[keys[selectedIndex]] is Dictionary<Object, Object>)
                            Open((Dictionary<Object, Object>)dict[keys[selectedIndex]], keys[selectedIndex].ToString());
                        else if (dict[keys[selectedIndex]] != null)
                            Edit(dict, keys[selectedIndex]);
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private static Object ParseInput(string input, Object oldValue)
        {
            if (oldValue is string)
                return input;

            else if (oldValue is UnencodedString) 
            {
                string formatedNumberString = input;
                if (oldValue.ToString().Contains("e") && double.TryParse(formatedNumberString, out double number))
                {
                    formatedNumberString = number.ToString("0.#####e+000");
                    ConsoleLogging.Log($"Parsed {input} to {formatedNumberString} to follow the data format!", LogLevel.Warning);
                }
                return new UnencodedString(formatedNumberString);
            }

            else if (oldValue is Single)
            {
                if (Single.TryParse(input, out float res))
                    return res;
            }
            else if (oldValue is Byte)
            {
                if (Byte.TryParse(input, out byte res))
                    return res;
            }
            else if (oldValue is Int16)
            {
                if (Int16.TryParse(input, out short res))
                    return res;
            }
            else if (oldValue is Int32)
            {
                if (Int32.TryParse(input, out int res))
                    return res;
            }
            else if (oldValue is Int64)
            {
                if (Int64.TryParse(input, out long res))
                    return res;
            }
            else if (oldValue is Boolean)
            {
                if (Boolean.TryParse(input, out bool res))
                    return res;
            }
            return null;
        }

        public static bool Edit(Dictionary<Object, Object> dict, Object key)
        {
            if (!dict.ContainsKey(key) || dict[key] == null || dict[key] is Dictionary<Object,Object>)
                return false;

            Console.Clear();

            ConsoleLogging.Log($"Editing {key.ToString()} = {dict[key].ToString()}({dict[key].GetType().Name}) | write 'exit' to go back.", LogLevel.Info);
            while (true) 
            {
                Console.Write($"\n{key.ToString()} = ");
                string res = Console.ReadLine().Trim();

                if (res.ToLower().Equals("exit"))
                    return false;
                Object finalValue = ParseInput(res, dict[key]);
                if (finalValue != null)
                {
                    if (Confirmation.Show($"Are you sure that you want to change the value of {key.ToString()} to {finalValue.ToString()}({finalValue.GetType().Name})") == ConfirmationResult.Yes)
                    {
                        dict[key] = finalValue;
                        ConsoleLogging.Log($"Set {key.ToString()} to {finalValue.ToString()}", LogLevel.Info);
                        Thread.Sleep(1000);
                        return true;
                    }
                } else
                    ConsoleLogging.Log($"Failed to parse input, please try again.", LogLevel.Error);
            }
        }
    }
}

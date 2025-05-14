using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD2.ConsoleUtils
{
    public enum ConfirmationResult
    {
        Yes,
        No
    }

    public static class Confirmation
    {
        public static ConfirmationResult Show(string message)
        {
            message = message.Trim();

            Console.Write($"\n{message} (y/N) : ");
            string res = Console.ReadLine().Trim().ToLower();
            
            if (res.Length > 0 && res.StartsWith("y"))
                return ConfirmationResult.Yes;

            return ConfirmationResult.No;
        }
    }
}

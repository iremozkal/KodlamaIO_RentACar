//using CarRent.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.ConsoleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleManager consoleManager = new ConsoleManager();
            consoleManager.MainScreen();

            Console.ReadKey();
        }
    }
}

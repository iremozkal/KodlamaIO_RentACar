using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager consoleManager = new ConsoleManager();
            consoleManager.MainScreen();

            Console.ReadKey();
        }
    }
}

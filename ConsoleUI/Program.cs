using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

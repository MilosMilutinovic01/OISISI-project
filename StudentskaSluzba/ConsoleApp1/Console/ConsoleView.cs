using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Console
{
    abstract class ConsoleView
    {
        protected int SafeInputInt()
        {
            int input;

            string rawInput = System.Console.ReadLine();

            while (!int.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Broj nije validan, pokusaj ponovo: ");

                rawInput = System.Console.ReadLine();
            }
            return input;
        }
    }
}

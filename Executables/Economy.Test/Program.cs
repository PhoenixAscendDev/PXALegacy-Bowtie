using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economy.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int value = 58;
            Console.WriteLine(JB2.Info.Project.ID);
            for(int i=0; i<10; i++)
            {
                value = JB2.Economy.JbeanStockExchange.CalculateNewStockValue(value);
                Console.WriteLine(value);
            }

            Console.ReadLine();
        }
    }
}

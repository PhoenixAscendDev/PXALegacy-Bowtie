using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int v = 5436;
            JB2.Bowtie.Jbean.Configure(null);
            Console.WriteLine(v.ToJBean().ToString());

            Console.ReadLine();
        }
    }
}

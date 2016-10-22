using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
using JB2.Common;

namespace JB2.Bowtie.Maintenance
{
    class Program
    {

        static void ConfigureBowtie()
        {
            JB2.Bowtie.Manager.Initialize("BT-1F4ACB33EAE78E37", JB2.Configuration.GetAppSetting("JB2:bowtie-apisecret"));
        }
        static void Main(string[] args)
        {

            ConfigureBowtie();
            var moduleService = new JB2.Bowtie.Service.ModuleService();

            var modules = moduleService.Retrieve();


            //Console.WriteLine(modules.Count());
            Console.ReadLine();
        }
    }
}

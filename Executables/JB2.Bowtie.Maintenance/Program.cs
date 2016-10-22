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

        static void SetupModules()
        {
            var moduleService = new JB2.Bowtie.Service.ModuleService();

            //jbean module
            IModule m = NonRestModule.New;
            m.Name = "jBean";
            List<string> propertyNames = new List<string>();
            propertyNames.Add("BANKACCOUNT");
            propertyNames.Add("STOCKMARKETACCOUNT");
            m.PlayerDataNames = propertyNames;

            moduleService.Save(m);

            //bluffstreet
            IModule bs = NonRestModule.New;
            bs.Name = "Bluff Street";
            propertyNames = new List<string>();
            //propertyNames.Add("BANKACCOUNT");
            //propertyNames.Add("STOCKMARKETACCOUNT");
            bs.PlayerDataNames = propertyNames;

            List<IInventoryItem> bsitems = new List<IInventoryItem>();

            bsitems.Add(new InventoryItem("bluffstreet-bronzeAcornd") { Name = "Bronze Acorns" });
            bsitems.Add(new InventoryItem("bluffstreet-crabclaw") { Name = "Crab Claws" });

            bs.InventoryItems = bsitems;
            moduleService.Save(bs);



        }
        static void Main(string[] args)
        {

            ConfigureBowtie();
            SetupModules();
            var moduleService = new JB2.Bowtie.Service.ModuleService();

            var modules = moduleService.Retrieve();
            //Console.WriteLine(modules.Count());
            Console.ReadLine();
        }
    }
}

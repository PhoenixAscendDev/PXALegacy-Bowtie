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
            m.PlayerDataNames = propertyNames;

            //moduleService.Save(m);


            //jbean stock market
            IModule sm = NonRestModule.New;
            m.Name = "jBean Stock Market";
            propertyNames = new List<string>();
            propertyNames.Add("STOCKMARKETACCOUNT");
            m.PlayerDataNames = propertyNames;

            var companies = JB2.Settings.JbeanStockMarket.Repository.GetAllCompanies();
            List<IInventoryItem> smitems = new List<IInventoryItem>();

            foreach (var c in companies)
            {
                smitems.Add(new InventoryItem("stockshare-" + c.StockSymbol) { Name = "Share of " + c.Name, PuralName = "Shares of " + c.Name, InventoryCategory = "StockShare" });
            }

            sm.PlayerDataNames = propertyNames;
            sm.InventoryItems = smitems;

            moduleService.Save(sm);





         

            //bluffstreet
            IModule bs = NonRestModule.New;
            bs.Name = "Bluff Street";
            propertyNames = new List<string>();
            propertyNames.Add("GUILD");
            propertyNames.Add("ASSIGNED-GUILDCARD");
            propertyNames.Add("COLLECTION-GUILDCARD");

            
            bs.PlayerDataNames = propertyNames;

            List<IInventoryItem> bsitems = new List<IInventoryItem>();

            bsitems.Add(new InventoryItem("bluffstreet-bronzeAcorns") { Name = "Bronze Acorn", PuralName = "Bronze Acorns", InventoryCategory = "Acorn" });
            bsitems.Add(new InventoryItem("bluffstreet-silverAcorns") { Name = "Silver Acorn", PuralName = "Silver Acorns", InventoryCategory = "Acorn" });
            bsitems.Add(new InventoryItem("bluffstreet-goldenAcorns") { Name = "Golden Acorn", PuralName = "Golden Acorns", InventoryCategory = "Acorn" });

            bsitems.Add(new InventoryItem("bluffstreet-crabclaw") { Name = "Crab Claw", PuralName = "Crab Claws", InventoryCategory = "BluffianHost" });
            bsitems.Add(new InventoryItem("bluffstreet-strawbundle") { Name = "Straw Bundle", PuralName="Straw Bundles", InventoryCategory="BluffianHost" });
            bsitems.Add(new InventoryItem("bluffstreet-owlfeather") { Name = "Owl Feather", PuralName = "Owl Feathers", InventoryCategory = "BluffianHost" });
            bsitems.Add(new InventoryItem("bluffstreet-caterpillarsilk") { Name = "Catapillar Silk", PuralName = "Catapillar Silks", InventoryCategory = "BluffianHost" });
            bsitems.Add(new InventoryItem("bluffstreet-pigtail") { Name = "Pig Tail", PuralName = "Pig Tails", InventoryCategory = "BluffianHost" });

            bsitems.Add(new InventoryItem("bluffstreet-wheelticket") { Name = "Wheel Ticket", PuralName = "Wheel Tickets", InventoryCategory = "Ticket" });

            for(var suit=1;suit <= 4;suit++)
            {
                for(var i = 1; i <= 13;i++)
                {
                    string suitName = string.Empty;
                    switch (suit)
                    {
                        case 1:
                            suitName = "heart";
                            break;
                        case 2:
                            suitName = "diamond";
                            break;
                        case 3:
                            suitName = "spade";
                            break;
                        case 4:
                            suitName = "club";
                            break;
                    }

                    bsitems.Add(new InventoryItem("bluffstreet-guildcard-" + i.ToString("D2") + suitName ) { Name = i.ToString() + " of " + suitName, PuralName = i.ToString() + " of " + suitName + "s", InventoryCategory = "GuildPlayingCard" });

                }
            }

            bs.InventoryItems = bsitems;
            //moduleService.Save(bs);



        }
        static void Main(string[] args)
        {

            ConfigureBowtie();
            SetupModules();
            //var moduleService = new JB2.Bowtie.Service.ModuleService();

            //var modules = moduleService.Retrieve();

            var companies = JB2.Settings.JbeanStockMarket.Repository.GetAllCompanies();


            Console.WriteLine(companies.Count());
            Console.ReadLine();
        }
    }
}

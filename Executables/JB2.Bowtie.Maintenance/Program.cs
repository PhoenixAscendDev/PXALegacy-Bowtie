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
            JB2.Bowtie.Web.Manager.Initialize("BT-35BC540F", JB2.Configuration.GetAppSetting("JB2:bowtie-apisecret"));
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


            //sprog module
            IModule s = NonRestModule.New;
            s.Name = "Sprog";
            propertyNames = new List<string>();
            propertyNames.Add("APIKEY");
            
            propertyNames.Add("SECRET");
            s.PlayerDataNames = propertyNames;

            moduleService.Save(s);








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

            //moduleService.Save(sm);


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

        static void SetupAuthRepo()
        {

            var appservice = new JB2.Bowtie.Service.ApplicationService();

            var apps = appservice.Retrieve();

            IAuthorizeRepository authRepo = JB2.Settings.Bowtie.UnitOfWork.AuthorizeRepository;

            foreach(var app in apps)
            {
                ApplicationStatePair asp = new ApplicationStatePair(app.ID, app.AuthorizedState);
                asp.ApplicationID = app.ID;
                asp.APIKey.APIkey = app.APIkey;
                asp.APIKey.Secret = app.Secret;
                asp.AuthorizeState = app.AuthorizedState;

                authRepo.Insert(asp);
                //authRepo.Save(Enum.APIAuthorizeState.Unknown, asp.ApplicationID);
                authRepo.UpdateAuthorizeState(app.AuthorizedState, app.ID);
            }

            Console.WriteLine("app count: " + apps.Count());

        }

        //static void AddJB2ID()
        //{
        //    var appservice = new JB2.Bowtie.Service.
        //}


        static void OnApplInitialized(IApplication application, string authorizeKey)
        {
            Console.WriteLine("App initialized: " + application.ID);
        }
        static void Main(string[] args)
        {
            JB2.Events.Bowtie.ApplicationInitilized += OnApplInitialized;
            ConfigureBowtie();

            var appService = new JB2.Bowtie.Service.ApplicationService();

           

            //SetupModules();

            //for(int i = 0;i<5;i++)
            //{
            //    Console.WriteLine(JB2.Helper.Graph.GenerateID<GraphAction>());
            //}
            ////setup Link Fence App
            //var linkfence = appService.GenerateNewApplication();
            //linkfence.Name = "Link Fence";
            //linkfence.Company = JB2.Info.HQ;
            //linkfence.Website = "http://linkfence.io";

            //appService.Save(linkfence);

            //var fivetwo = appService.GenerateNewApplication();
            //fivetwo.Name = "Grateful Five|Two";
            //fivetwo.Company = JB2.Info.HQ;
            //fivetwo.Website = "http://fivetwo.io";

            //appService.Save(fivetwo);


            //var allApps = appService.Retrieve();

            //foreach (var a in allApps)
            //{

            //    Console.WriteLine("app:" + a.Name + "(" + a.ID + ")");
            //    a.Company = JB2.Info.HQ;
            //    appService.Save(a);


            //}

            //var app = appService.GenerateNewApplication();

            //app.Name = "Bowtie Maintenance";

            //appService.Save(app);


            //Console.WriteLine(app.APIkey);
            //Console.WriteLine(app.Secret);


            //SetupAuthRepo();

            //Console.WriteLine(JB2.Settings.Bowtie.isAuthorized());

            //var appService = new JB2.Bowtie.Service.ApplicationService();

            //var apps = appService.Retrieve();

            //foreach(var app in apps)
            //{
            //    appService.Save(app);
            //}


            //var newApp = appService.GenerateNewApplication();
            //newApp.Company = JB2.Info.HQ;
            //newApp.Name = "TestApp";

            //appService.Save(newApp);

            //for (int i=0; i< 3;i++)
            //{
            //    var id = JB2.Common.NewID.ProductID(4);
            //    Console.WriteLine(id);
            //}

            Console.ReadLine();
        }
    }
}

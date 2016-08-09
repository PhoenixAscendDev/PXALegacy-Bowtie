using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JB2.Bowtie;
using JB2.Bowtie.Enum;

using JB2.Economy;
namespace Bowtie.ConsoleTest
{
    class Program
    {

        static void AddRegisterGraphAction()
        {

            var dewdropService = new JB2.Bowtie.Service.DewdropService();
            var graphService = new JB2.Bowtie.Service.GraphService();

            var allProps = graphService.RetreiveProperties();

            var jbeanCostProp = GraphProperty.NewProperty("jb2:jbean_cost", GraphPropertyType.Number, string.Empty, false);

            var registerAction = GraphAction.NewAction("PlayerRegister", string.Empty);


            registerAction.AddProperty(jbeanCostProp);

            foreach (var prop in allProps)
            {
                switch (prop.PropertyName)
                {
                    case "jb2:app_id":
                    case "bt:start_time":
                    case "jb2:profile_id":
                        registerAction.AddProperty(prop);
                        break;
                }
            }
            graphService.SaveProperty(jbeanCostProp);
            graphService.SaveAction(registerAction);
        }

        static void AchievementReg()
        {
            var app = JB2.Settings.Bowtie.CurrentApplication;
            var registerAchievement = new LapelPinAchievement();

            registerAchievement.Name = "First Signin";
            registerAchievement.ApplicationID = app.GetID();
            registerAchievement.DewdropTriggers = new string[1] { "dew_4CBe" };
            registerAchievement.Description = "Signed into Five|Two for the first time";
            registerAchievement.StepFx = "REGISTER";
            registerAchievement.StepsRequired = 1;
            registerAchievement.StepType = StepFxType.RegexMatchSingle;
            registerAchievement.Points = 1000;

            var achievementService = new JB2.Bowtie.Service.AchievementService();
            achievementService.Save(registerAchievement);


        }



        

        static void Main(string[] args)
        {
            
            JB2.Bowtie.Manager.Initialize("BT-BDF1FC3E51F48224", "912473a6-8c31-4ecf-9d5c-1af07c1b8ef3");
            Console.WriteLine(JB2.Settings.Jbean.GetTokenValue(JB2.Economy.Enum.JBeanTokenType.Pinto).ToString());


            var app = JB2.Settings.Bowtie.CurrentApplication;

            var playerService = new JB2.Bowtie.Service.PlayerService();



            var tplayer = playerService.RetrieveById("i-febble");

            playerService.RegisterPlayer(tplayer, app, "JBID");



            //new Register Achievements

 






            //AddRegisterGraphAction();

            //var dewdropService = new JB2.Bowtie.Service.DewdropService();

            //var d1 = Dewdrop.NewDewdrop("RegisterNewPlayer", "Player account has been created", "SV-001", "a_jQfgLjIiYU2Oidw-aeB1Qg",-1000);



            //dewdropService.Save(d1);



            //var pservice = new JB2.Bowtie.Service.PlayerService();

            //var player = pservice.RetrieveByAuthID("i-febble", JB2.Settings.Bowtie.CurrentApplication);
            //var app = JB2.Settings.Bowtie.CurrentApplication;

            //JB2.Bowtie.Service.WalletService wservice = new JB2.Bowtie.Service.WalletService();


            //var wallet = wservice.RetrieveWalletByPlayer(player, app);

            //wservice.AddJBeansToWallet(wallet, 100);

            //wservice.RemoveJBeansToWallet(wallet, 10);

            //Console.WriteLine(wallet.JBeanTotal.ToString());
            Console.ReadLine();
        }

        //static async Task RunAsync()
        //{

        //    Console.WriteLine("Calling the back-end API");

        //    //string apiBaseAddress = JB2.Bowtie.Settings.APIInfo.URL;
        //    string apiBaseAddress = "http://localhost:59536/";

        //    BowtieDelegatingHandler customDelegatingHandler = new BowtieDelegatingHandler();

        //    HttpClient client = HttpClientFactory.Create(customDelegatingHandler);

        //    var command = new GameCommand();

        //    HttpResponseMessage response = await client.PostAsJsonAsync(apiBaseAddress + "api/v1/command/add", command);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseString = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(responseString);
        //        Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
        //    }

        //    Console.ReadLine();
        //}
    }
}

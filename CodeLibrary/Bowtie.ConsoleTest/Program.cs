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
        static void Main(string[] args)
        {
            
            JB2.Bowtie.Manager.Initialize("4d53bce03ec34c0a911182d4c228ee6d", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");

            var graphService = new JB2.Bowtie.Service.GraphService();
            //var uofw = JB2.Settings.Bowtie.UnitOfWork;

            var p1 = GraphProperty.NewProperty("og:url", GraphPropertyType.Uri,string.Empty, false);
            var p2 = GraphProperty.NewProperty("og:type", GraphPropertyType.Text, string.Empty, false);
            var p3 = GraphProperty.NewProperty("og:title", GraphPropertyType.Text, string.Empty, false);
            var p4 = GraphProperty.NewProperty("og:image", GraphPropertyType.Image, string.Empty, false);
            var p5 = GraphProperty.NewProperty("og:locale", GraphPropertyType.Locale, string.Empty, false);
            var p13 = GraphProperty.NewProperty("og:description", GraphPropertyType.Text, string.Empty, false);
            var p14 = GraphProperty.NewProperty("og:updated_time", GraphPropertyType.Datetime, string.Empty, false);
            var p15 = GraphProperty.NewProperty("og:video", GraphPropertyType.Video, string.Empty, false);
            var p16 = GraphProperty.NewProperty("og:audio", GraphPropertyType.Audio, string.Empty, false);

            
            var p6 = GraphProperty.NewProperty("jb2:app_id", GraphPropertyType.Text, string.Empty, false);
            var p7 = GraphProperty.NewProperty("jb2:profile_id", GraphPropertyType.Profile, string.Empty, false);
            var p8 = GraphProperty.NewProperty("bt:start_time", GraphPropertyType.Datetime, string.Empty, false);
            var p9 = GraphProperty.NewProperty("bt:end_time", GraphPropertyType.Datetime, string.Empty, false);
            var p10 = GraphProperty.NewProperty("bt:expires_time", GraphPropertyType.Datetime, string.Empty, false);
            var p11 = GraphProperty.NewProperty("bt:expires_in", GraphPropertyType.Number, string.Empty, false);
            var p12 = GraphProperty.NewProperty("bt:message", GraphPropertyType.Number, string.Empty, false);

            var p17 = GraphProperty.NewProperty("bt:color", GraphPropertyType.Color, string.Empty, false);
            var p18 = GraphProperty.NewProperty("picket:redirect_url", GraphPropertyType.Uri, "LF-001", false);
            var p19 = GraphProperty.NewProperty("picket:picket_style", GraphPropertyType.Text, "LF-001", false);
            var p20 = GraphProperty.NewProperty("picket:code", GraphPropertyType.Text, "LF-001", false);

            var o1 = GraphObject.NewObject("Picket", "LF-001", "A", "pickets");
            o1.AddProperty(p18);
            o1.AddProperty(p19);
            o1.AddProperty(p20);

            //graphService.SaveProperty(p1);
            //graphService.SaveProperty(p2);
            //graphService.SaveProperty(p3);
            //graphService.SaveProperty(p4);
            //graphService.SaveProperty(p5);
            //graphService.SaveProperty(p6);
            //graphService.SaveProperty(p7);
            //graphService.SaveProperty(p8);
            //graphService.SaveProperty(p9);
            //graphService.SaveProperty(p10);
            //graphService.SaveProperty(p11);
            //graphService.SaveProperty(p12);
            //graphService.SaveProperty(p13);
            //graphService.SaveProperty(p14);
            //graphService.SaveProperty(p15);
            //graphService.SaveProperty(p16);
            //graphService.SaveProperty(p17);
            //graphService.SaveProperty(p18);
            //graphService.SaveProperty(p19);
            //graphService.SaveProperty(p20);
            //graphService.SaveObject(o1);

            var g = graphService.RetrieveObjectByName("Picket");
            Console.WriteLine(graphService.RetreiveProperties().Count());

            

            //int test = 3503;

            //Console.WriteLine(test.ToJBean().ToString());
            //Console.WriteLine(JB2.Settings.Bowtie.CurrentApplication.ToString());

            //Console.WriteLine(JB2.Settings.Jbean.GetSetting(JB2.Economy.JbeanSettingName.CurrencyID).Value.ToString());
            ////var debugPath = string.Empty;
            //#if (DEBUG)
            //    debugPath = "..\\..\\";
            //#endif


            //string settingsFilePath =  System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, debugPath) + "bowtieApp_v1.json";

            //BowtieConfig settings = BowtieConfig.Load(settingsFilePath);

            //JB2.Bowtie.Manager.Initialize(settingsFilePath);

            //JB2.Bowtie.Service.GameObjectService service = new JB2.Bowtie.Service.GameObjectService(null, new JB2.Bowtie.Data.Azure.GameObjectRepository());

            //IBingoBalDeck<byte> queue = service.RetrieveRandomBingoDeck(JB2.Bowtie.Enum.BingoType.Standard);

            ////Console.WriteLine(queue.Peek().ToString());

            //Console.WriteLine(PlayingCardSuit.Club.isBlack);

            //PlayingCardDeck cards = CardHelper.GeneratePlayingCardDeck(true, true, 2);





            //IBingoCard card = BingoHelper.GenerateNewBingoCard(JB2.Bowtie.Enum.BingoType.Standard);

            //Console.WriteLine(BingoHelper.CalculateChecksum(card.Cells));
            //Console.ReadLine();
            //string str2 = JB2.Common.Utility.ObjectToString(card.Cells);

            //JB2.Bowtie.Data.Azure.GameObjectRepository repo = new JB2.Bowtie.Data.Azure.GameObjectRepository();

            //JB2.Bowtie.Service.GameObjectService service = new JB2.Bowtie.Service.GameObjectService(null,repo);

            //IBingoCard carddata = service.RetrieveBingoCardByID("STA-4925-282-2082-7-70-TMuk");

            //Console.WriteLine(str2);

            //char[,] ar2 = { { '1', '2', '3' }, { 'a', 'b', 'c' } };

            //string str2 = JB2.Common.Utility.ObjectToString(ar2);
            //Console.WriteLine(str2);
            //Console.ReadLine();

            //char[,] ar3 = JB2.Common.Utility.ObjectFromString(str2) as char[,];

            //Console.WriteLine(ar3);
            //Console.ReadLine();
            //RunAsync().Wait();
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

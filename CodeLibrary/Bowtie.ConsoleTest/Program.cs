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
using JB2.Bowtie.GameObjects;

namespace Bowtie.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //JB2.Bowtie.Manager.Initialize("4d53bce03ec34c0a911182d4c228ee6d", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");


            var debugPath = string.Empty;
            #if (DEBUG)
                debugPath = "..\\..\\";
            #endif


            string settingsFilePath =  System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, debugPath) + "bowtieApp_v1.json";

            BowtieConfig settings = BowtieConfig.Load(settingsFilePath);

            JB2.Bowtie.Manager.Initialize(settingsFilePath);

            JB2.Bowtie.Service.GameObjectService service = new JB2.Bowtie.Service.GameObjectService(null, new JB2.Bowtie.Data.Azure.GameObjectRepository());

            //IColorSet set = service.RetrieveColorSet(JB2.Bowtie.Enum.ColorSetType.CrayolaStandard);
            //IColor color = service.RetrieveColorByHex("6B4106");
            //color.ColorSet = JB2.Bowtie.Enum.ColorSetType.CrayolaStandard;

            //ColorSet set = new ColorSet();

            //set.Name = "Crayola Standard";
            //set.Type = JB2.Bowtie.Enum.ColorSetType.CrayolaStandard;
            //set.AddColor(color);

            // service.SaveColorSet(set);

            IColor color = service.RetrieveColorByName("Maroon", JB2.Bowtie.Enum.ColorSetType.CrayolaStandard);

            JB2.Common.JB2Color cColor2 = JB2.Common.ColorHelper.GetComplementaryColor(color.Color);

            JB2.Common.JB2Color[] colors = JB2.Common.ColorHelper.GetAdjacentColors(color.Color);

            JB2.Common.JB2Color[] triad = JB2.Common.ColorHelper.GetTriadColors(color.Color);

            JB2.Common.JB2Color[] split = JB2.Common.ColorHelper.GetSplitComplementaryColors(color.Color);

            JB2.Common.JB2Color[] tetrad = JB2.Common.ColorHelper.GetTetradColors(color.Color);

            Console.WriteLine(color.HexValue);
            




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

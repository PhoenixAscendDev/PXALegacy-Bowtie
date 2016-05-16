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
            
            JB2.Bowtie.Manager.Initialize("BT-BDF1FC3E51F48224", "912473a6-8c31-4ecf-9d5c-1af07c1b8ef3");
            Console.WriteLine(JB2.Settings.Jbean.GetTokenValue(JB2.Economy.Enum.JBeanTokenType.Pinto).ToString());

            var pservice = new JB2.Bowtie.Service.PlayerService();

            var player = pservice.RetrieveByAuthID("i-febble", JB2.Settings.Bowtie.CurrentApplication);
            var app = JB2.Settings.Bowtie.CurrentApplication;

            JB2.Bowtie.Service.WalletService wservice = new JB2.Bowtie.Service.WalletService();


            var wallet = wservice.RetrieveWalletByPlayer(player, app);

            wservice.AddJBeansToWallet(wallet, 100);

            wservice.RemoveJBeansToWallet(wallet, 10);

            Console.WriteLine(wallet.JBeanTotal.ToString());
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

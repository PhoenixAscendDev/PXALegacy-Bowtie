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


            string settingsFilePath =  System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, debugPath) + "bowtieApp_v1.config";

            BowtieConfig settings = BowtieConfig.Load(settingsFilePath);



            JB2.Bowtie.Manager.Initialize(settingsFilePath);


            RunAsync().Wait();
            
        }

        static async Task RunAsync()
        {

            Console.WriteLine("Calling the back-end API");

            string apiBaseAddress = "http://jb2-bowtie.azurewebsites.net/"; //"http://localhost:52151/";
            //string apiBaseAddress = "http://localhost:52151/";

            BowtieDelegatingHandler customDelegatingHandler = new BowtieDelegatingHandler();

            HttpClient client = HttpClientFactory.Create(customDelegatingHandler);

            var command = new GameCommand();

            HttpResponseMessage response = await client.PostAsJsonAsync(apiBaseAddress + "api/v1/commands", command);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
            }
            else
            {
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
            }

            Console.ReadLine();
        }
    }
}

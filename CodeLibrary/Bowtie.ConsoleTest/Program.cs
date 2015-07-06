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
            JB2.Bowtie.Manager.Initialize("F79676FF52F9018B4FC1BEE5E0.battlesim", "HuCSuRTYGyJhMJbTjsCU4O6YemrZLOrwoGJLMe5CnSE=");
            RunAsync().Wait();
            
        }

        static async Task RunAsync()
        {

            Console.WriteLine("Calling the back-end API");

            string apiBaseAddress = "http://localhost:52151/";

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

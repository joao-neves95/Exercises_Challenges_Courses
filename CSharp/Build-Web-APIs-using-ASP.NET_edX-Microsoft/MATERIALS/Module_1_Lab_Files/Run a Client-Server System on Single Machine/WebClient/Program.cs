using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebClient {
    class Program {
        static void Main(string[] args) {
            string address = "http://localhost:5000";
            if (args != null && args.Length > 0) {
                address = args[0];
            }

            MainAsync(address).Wait();
        }

        static async Task MainAsync(string address) {
            var httpClient = GetHttpClient(address);
            while (true) {
                Console.WriteLine("Acceptable input: integer | ALL | EXIT");
                var input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input)) {
                    continue;
                } else if (Int32.TryParse(input, out int id)) {
                    var path = $"api/rockets/{id}";
                    var rocket = await GetRocketAsync(httpClient, path);
                    PrintRocket(rocket);
                } else if (String.Equals(input, "ALL", StringComparison.CurrentCultureIgnoreCase)) {
                    var path = "api/rockets";
                    var rockets = await GetRocketsAsync(httpClient, path);
                    PrintRockets(rockets);
                } else if (String.Equals(input, "EXIT", StringComparison.CurrentCultureIgnoreCase)) {
                    System.Console.WriteLine("Thank You!");
                    break;
                }
            }
        }

        static HttpClient GetHttpClient(string address) {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(address);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        static async Task<Rocket> GetRocketAsync(HttpClient client, string path) {
            Rocket rocket = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                rocket = JsonConvert.DeserializeObject<Rocket>(jsonString);
            }

            return rocket;
        }

        static async Task<Rocket[]> GetRocketsAsync(HttpClient client, string path) {
            Rocket[] rockets = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                var jsonString = await response.Content.ReadAsStringAsync();
                rockets = JsonConvert.DeserializeObject<Rocket[]>(jsonString);
            }

            return rockets;
        }

        static void PrintRocket(Rocket rocket) {
            if (rocket == null) {
                System.Console.WriteLine("Nothing to print.");
            } else {
                System.Console.WriteLine($"ID:{rocket.ID}\tBuilder:{rocket.Builder}\tTarget:{rocket.Target}\tSpeed:{rocket.Speed}");
            }
        }

        static void PrintRockets(IEnumerable<Rocket> rockets) {
            if (rockets == null) {
                System.Console.WriteLine("Nothing to print.");
            } else {
                foreach (var rocket in rockets) {
                    PrintRocket(rocket);
                }
            }
        }
    }

    public class Rocket {
        public int ID { get; set; }
        public string Builder { get; set; }
        public string Target { get; set; }
        public double Speed { get; set; }
    }
}

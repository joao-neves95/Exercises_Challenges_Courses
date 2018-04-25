using System;
using Newtonsoft.Json;

namespace SeDes
{
    class Program
    {
        static void Main(string[] args)
        {
            Rocket[] rockets = {
                new Rocket { ID = 0, Builder = "NASA", Target = "Moon", Speed = 7.8 },
                new Rocket { ID = 1, Builder = "NASA", Target = "Mars", Speed = 10.9 },
                new Rocket { ID = 2, Builder = "NASA", Target = "Kepler-452b", Speed = 42.1 },
                new Rocket { ID = 3, Builder = "NASA", Target = "N/A", Speed = 0 },
            };
            
            string rocketsJSONString = DoSerialization(rockets);
            PrintRocketInfo(rocketsJSONString);

            Console.WriteLine("==================================");

            Rocket[] rocketJSONObject = DoDeserialization(rocketsJSONString);
            PrintRocketInfo(rocketJSONObject);
        }

        // Serialize a Rocket array into a JSON string.
        public static string DoSerialization(Rocket[] rockets) {
            string json = JsonConvert.SerializeObject(rockets);
            return json;
        }

        // Deserialize a JSON string back to a Rocket array.
        public static Rocket[] DoDeserialization(string json) {
            Rocket[] rockets = JsonConvert.DeserializeObject<Rocket[]>(json);
            return rockets;
        }

        public static void PrintRocketInfo(Rocket[] rockets) {
            foreach (Rocket rocket in rockets) {
                Console.WriteLine($"ID: { rocket.ID }, Builder: { rocket.Builder }, Target: { rocket.Target }, Speed: { rocket.Speed }.");
            }
        }

        public static void PrintRocketInfo(string rockets) {
            Console.WriteLine(rockets);
        }
    }
}

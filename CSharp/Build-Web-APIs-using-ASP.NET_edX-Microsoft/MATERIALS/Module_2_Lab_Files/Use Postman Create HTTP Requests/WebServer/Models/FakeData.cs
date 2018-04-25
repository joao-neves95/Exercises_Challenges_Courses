using System.Collections.Generic;

namespace WebServer.Models {
    public class FakeData {
        public static IDictionary<int, Rocket> Rockets { get; private set; }

        static FakeData() {
            Rockets = new Dictionary<int, Rocket>();
            Rockets.Add(0, new Rocket { ID = 0, Builder = "NASA", Target = "Moon", Speed = 7.8 });
            Rockets.Add(1, new Rocket { ID = 1, Builder = "NASA", Target = "Mars", Speed = 10.9 });
            Rockets.Add(2, new Rocket { ID = 2, Builder = "NASA", Target = "Jupiter", Speed = 42.1 });
            Rockets.Add(3, new Rocket { ID = 3, Builder = "NASA", Target = "Saturn", Speed = 0.0 });
        }
    }
}
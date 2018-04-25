using System.Collections.Generic;

namespace WebServer.Models {
    public class FakeData {
        public static IList<Rocket> Rockets { get; set; }

        static FakeData() {
            Rockets = new List<Rocket>();
            Rockets.Add(new Rocket { ID = 0, Builder = "NASA", Target = "Moon", Speed = 7.8 });
            Rockets.Add(new Rocket { ID = 1, Builder = "NASA", Target = "Mars", Speed = 10.9 });
            Rockets.Add(new Rocket { ID = 2, Builder = "NASA", Target = "Jupiter", Speed = 42.1 });
            Rockets.Add(new Rocket { ID = 3, Builder = "NASA", Target = "Saturn", Speed = 0.0 });
        }
    }
}
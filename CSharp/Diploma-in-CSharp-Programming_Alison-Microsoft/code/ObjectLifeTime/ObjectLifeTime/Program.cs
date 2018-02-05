using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLifeTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car("Nissan");
            Car otherCar = new Car("Tesla", "X", 2018, "Silver");

            Console.WriteLine(myCar.ToString());
            Console.WriteLine(otherCar.ToString());
            Console.ReadLine();

            // Explicitly setting the object instance (reference) of Car to null:
            otherCar = null;
            myCar = null;
        }
    }

    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double OriginalPrice { get; set; }

        public Car(string make)
        {
            this.Make = make;
        }

        public Car(string make, string model, int year, string color)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Color = color;
        }

        public static void SomeStaticMethod
        {
        }
    }
}

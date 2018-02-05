using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Car myNewCar = new Car();
            myNewCar.Make = "Oldsmobile";
            myNewCar.Model = "Cutlas Supreme";
            myNewCar.Year = 1999;
            myNewCar.Color = "Silver";

            //Console.WriteLine($"My new car is from the maker \"{myNewCar.Make}\", " +
            //    $"is the model \"{myNewCar.Model}\", it's {myNewCar.Color}, and costs {myNewCar.CarValue}");

            //DetermineMarketValue(myNewCar);
            //Console.WriteLine($"My new car is from the maker \"{myNewCar.Make}\", " +
            //    $"is the model \"{myNewCar.Model}\", it's {myNewCar.Color}, and costs {myNewCar.CarValue}");

            Console.WriteLine(myNewCar.TellAllOfThis());
            Console.ReadLine();
        }

        //private static object DetermineMarketValue(Car car)
        //{
        //    double carValue = 100.00;
        //    car.CarValue = carValue;
        //    return car;
        //}
    }

    class Car
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public double CarValue { get; set; }

        private double DetermineMarketValue()
        {
            double carValue = 20000.00;
            if (this.Year < 1995)
                carValue *= 0.30;
            else if (this.Year < 2005)
                carValue *= 0.45;
            else
                carValue *= 0.60;

            return carValue;
        }

        public string TellAllOfThis()
        {
            return $"My new car is from the maker \"{this.Make}\", " +
                $"is the model \"{this.Model}\", it's {this.Color}, " +
                $"it's from {this.Year} and costs {this.DetermineMarketValue():C}";
        }
    }
}

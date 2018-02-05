using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car() {
                Make = "BMW",
                Model = "745li",
                Color = "Black",
                Year = 2005
            };

            PrintVehicleDetails(myCar);

            Truck myTruck = new Truck() {
                Make = "Ford",
                Model = "F950",
                Color = "White",
                Year = 2006,
                TowingCapacity = 12000
            };

            PrintVehicleDetails(myTruck);

            Console.ReadLine();
        }

        /// <summary>
        /// Takes the instance of the Vehicle class, get's their details, and print's them to the console.
        /// </summary>
        /// <param name="vehicle">Instance of the Vehicle class</param>
        /// <returns>void</returns>
        private static void PrintVehicleDetails(Vehicle vehicle)
        {
            Console.WriteLine("Here are the Vehicle's details: {0}",
                vehicle.FormatMe()
            );
        }
    }

    abstract class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        // "virtual", used to (optionaly) accept override.
        // "abstract", used to force override in children classes.

        // "abstract": 'You can't create an instance of FormatMe(),
        // but you have to have an implementation of FormatMe().'
        public abstract string FormatMe();
    }

    class Car : Vehicle
    {
        public override string FormatMe()
        {
            return String.Format("{0} - {1} - {2} - {3}",
                this.Make,
                this.Model,
                this.Color,
                this.Year
            );
        }
    }

    // "sealed": Nothing on this type of classes can be inherited by another child class.
    sealed class Truck : Car
    {
        public int TowingCapacity { get; set; }

        // "override", used to override the default FormatMe() method inherited from the parent (Car) class.
        public override string FormatMe()
        {
            return String.Format("{0} - {1} - {2} - {3} - Towing Capacity: {4}",
                this.Make,
                this.Model,
                this.Color,
                this.Year,
                this.TowingCapacity
            );
        }
    }
}

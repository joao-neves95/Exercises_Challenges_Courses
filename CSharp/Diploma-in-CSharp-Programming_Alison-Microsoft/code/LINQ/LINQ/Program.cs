using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>() {
                new Car() { Make = "BMW", Model = "550i", Color = CarColor.Blue, StickerPrice = 55000, Year = 2009 },
                new Car() { Make = "Toyota", Model = "4Runner", Color = CarColor.White, StickerPrice = 35000, Year = 2010 },
                new Car() { Make = "BMW", Model = "745li", Color = CarColor.Black, StickerPrice = 75000, Year = 2008 },
                new Car() { Make = "Ford", Model = "Escape", Color = CarColor.White, StickerPrice = 25000, Year = 2008 },
                new Car() { Make = "BMW", Model = "55i", Color = CarColor.Black, StickerPrice = 57000, Year = 2010 }
            };

            /*
            var bmw2008 = from car in cars
                          where car.Make == "BMW" && car.Year == 2008
                          select car;

            var bmw2008Model = from car in cars
                               where car.Make == "BMW" && car.Year == 2008
                               select new { car.Model };

            var cheapest = from car in cars
                           where car.StickerPrice < 50000
                           select car;
            */

            /*
            var orderedCars = from car in cars
                              orderby car.Year descending
                              select car;

            var bmwCount = (from car in cars
                          where car.Make == "BMW"
                          select car).Count();
            */
            // Method Syntax:
            // var _bmws = cars.Where(car => car.Year == 2010).Where(car => car.Make == "BMW");

            var _orderedCars = cars.OrderByDescending(p => p.Year);

            var sum = cars.Sum(p => p.StickerPrice);
            Console.WriteLine(sum);

            foreach (var car in _orderedCars)
            {
                Console.WriteLine($"{car.Make} - {car.Model} - {car.Year}");
            }

            Console.ReadLine();
        }
    }
}

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double StickerPrice { get; set; }
    public CarColor Color { get; set; }
}

enum CarColor
{
    Blue,
    Black,
    White,
    Red,
    Yellow
}

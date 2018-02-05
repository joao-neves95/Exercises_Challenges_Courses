using System;
using System.Collections.Generic;

namespace WorkingWithCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car()
            {
                Make = "BMW",
                Model = "M3"
            };

            Car car2 = new Car()
            {
                Make = "Audi",
                Model = "A3"
            };

            Books book1 = new Books()
            {
                Author = "Robert Tabor",
                Title = "Microsoft .NET XML Web Services",
                ISBN = "0-000-00000-0"
            };

            /* LISTS:
             * 
            List<Car> carList = new List<Car>();
            carList.Add(car1);
            carList.Add(car2);
            
            foreach (Car car in carList)
            {
                Console.WriteLine(car.Model);
            }

            Console.WriteLine(carList[0].Make)
            */

            /* Dictionaries:
             *
            Dictionary<string, Car> carDicionary = new Dictionary<string, Car>();
            carDicionary.Add(car1.Make, car1);
            carDicionary.Add(car2.Make, car2);

            Console.WriteLine(carDicionary["Audi"].Model);
            */

            //string[] names = { "Bob", "Steve", "Brian", "Chuck" };
            //Car car_1 = new Car() { Make = "BMW", Model = "M3" };

            List<Car> carList = new List<Car>()
            {
                new Car { Make = "BMW", Model = "M3" },
                new Car { Make = "Audi", Model = "A3"},
                new Car { Make = "Seat", Model = "Ibiza"}
            };

            Console.ReadLine();
        }
    }

    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class Books
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
    }
}

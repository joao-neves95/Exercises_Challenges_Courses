using System;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using EntityFrameworkCore.Data;

namespace EntityFrameworkCore
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("");

            SeedData.Init();
            SeedData.ShowAll();
        }
    }
}

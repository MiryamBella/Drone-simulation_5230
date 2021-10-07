using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6400();
            Welcome5230();
            Console.ReadKey();
        }
        static partial void Welcome5230();
        private static void Welcome6400()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}

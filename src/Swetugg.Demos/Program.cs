using System;

namespace Swetugg.Demos
{
    class Boo { }

    class Program
    {
        private const string hello = "hello";

        static void Main(string[] args)
        {
            const string world = "world";
            Console.WriteLine($"{hello} {world}");

            String text = "hello";
            int index = 100;
            Console.WriteLine($"{text} {index}");

            foreach (var arg in args) { Print(arg); }
            var number = (int)(object)args[0];
            var differentNumber = (number + number) * 4;
        }

        private static void Print(string message)
        {
            Console.WriteLine($"Message: {message}!");

            try
            {

            } catch (Exception e)
            {
                throw e;
            }
        }

        private static void StupidAllocation()
        {
            int number = 1;
            object obj = number;
            int number2 = (int)obj;
        }
    }
}

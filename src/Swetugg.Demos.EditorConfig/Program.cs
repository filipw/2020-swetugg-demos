using System;

namespace Swetugg.Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args) 
            {
                Console.WriteLine(arg); 
            }

            var number = (int)(object)args[0];

            Console.WriteLine(number * number);
            Console.ReadLine();
        }
	}
}
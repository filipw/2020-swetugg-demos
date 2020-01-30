using System;

namespace Swetugg.Demos
{
    class Dog
    {
        private const string woofwoof = "Woof woof!";

        public void Bark()
        {
            const string barks = "dog barks";
            Console.WriteLine($"{woofwoof} {barks}!");
        }
    }
}
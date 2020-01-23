using System;

namespace Swetugg.Demos
{
    class Dog
    {
        private const string WOOFWOOF = "Woof woof!";

        public void Bark()
        {
            const string BARKS = "dog barks";
            Console.WriteLine($"{WOOFWOOF} {BARKS}!");
        }
    }
}
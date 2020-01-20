using System;
using System.Collections.Generic;
using System.Text;

namespace Swetugg.Demos
{
    class Dog
    {
        private string _name;

        public Dog(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public async void Bark()
        {
            Console.WriteLine($"Woof woof! {_name} barks!");
        }
    }

    class Cat { }
}

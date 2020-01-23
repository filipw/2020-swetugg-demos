using System;

namespace Swetugg.Demos
{
    class Person
    {
        public Person(String name, Int32 age)
        {
            Name = name;
            Age = age;
        }

        public String Name { get; }

        public Int32 Age { get; }
    }
}
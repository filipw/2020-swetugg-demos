using System;

namespace Swetugg.Demos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var dog = new Dog();
                var cat = new Cat();
            }
            catch (Exception)
            {
                // do nothing
            }
        }
    }
}

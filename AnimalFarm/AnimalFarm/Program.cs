namespace AnimalFarm
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using AnimalFarm.Models;
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            Type chickenType = typeof(Chicken);
            FieldInfo[] fields = chickenType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo[] methods = chickenType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(fields.Where(f => f.IsPrivate).Count() == 2);
            Debug.Assert(methods.Where(m => m.IsPrivate).Count() == 1);
            try
            {
                Chicken chicken = new Chicken(name, age);
                Console.WriteLine(chicken.ToString());
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

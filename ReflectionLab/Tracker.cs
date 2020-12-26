using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionLab
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type type = typeof(ReflectionLab.Spy);
            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Static);

            foreach (var method in methodInfos)
            {
                if (method.CustomAttributes.Any(n=>n.AttributeType==typeof(AuthorAttribute)))
                {
                    var attributes = method.GetCustomAttributes(true);
                    foreach (AuthorAttribute item in attributes)
                    {
                        Console.WriteLine($"{method.Name} is written by {item.Name}");
                    }
                }
            }
        }
    }
}

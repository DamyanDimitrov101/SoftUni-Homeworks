using System;
using System.Collections.Generic;
using System.Text;

namespace TestsAqua
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;
    using AquaShop;

    // ReSharper disable InconsistentNaming
    // ReSharper disable CheckNamespace

    public class Tests_000_003
    {
        // MUST exist within project, otherwise a Compile Time Error will be thrown.
        private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

        [Test]
        public void ValidateAddFishCommandMethodInController()
        {
            var controller = CreateObjectInstance(GetType("Controller"));

            var addAquariumArguments = new object[] { "FreshwaterAquarium", "Hush" };
            InvokeMethod(controller, "AddAquarium", addAquariumArguments);

            var addFishArguments = new object[] { "Hush", "FreshwaterFish", "Dorry", "Salmon", 10.12m };
            var validActualResult = InvokeMethod(controller, "AddFish", addFishArguments);

            var validExpectedResult = "Successfully added FreshwaterFish to Hush.";

            Assert.AreEqual(validExpectedResult, validActualResult);
        }

        private static object InvokeMethod(object obj, string methodName, object[] parameters)
        {
            try
            {
                var result = obj.GetType()
                    .GetMethod(methodName)
                    .Invoke(obj, parameters);

                return result;
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException.Message;
            }
        }

        private static object CreateObjectInstance(Type type, params object[] parameters)
        {
            try
            {
                var desiredConstructor = type.GetConstructors()
                    .FirstOrDefault(x => x.GetParameters().Any());

                if (desiredConstructor == null)
                {
                    return Activator.CreateInstance(type, parameters);
                }

                var instances = new List<object>();

                foreach (var parameterInfo in desiredConstructor.GetParameters())
                {
                    var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                    instances.Add(currentInstance);
                }

                return Activator.CreateInstance(type, instances.ToArray());
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException.Message;
            }
        }

        private static Type GetType(string name)
        {
            var type = ProjectAssembly
                .GetTypes()
                .FirstOrDefault(t => t.Name.Contains(name));

            return type;
        }
    }
}

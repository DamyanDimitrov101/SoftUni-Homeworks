using NUnit.Framework;

namespace TestsAqua
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;
    using AquaShop;
    public class Tests
    {

        // ReSharper disable InconsistentNaming
        // ReSharper disable CheckNamespace

        public class Tests_000_002
        {
            // MUST exist within project, otherwise a Compile Time Error will be thrown.
            private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

            [Test]
            public void ValidateAddDecorationCommandMethodInController()
            {
                var controller = CreateObjectInstance(GetType("Controller"));

                var addDecorationArguments = new object[] { "Ornament" };
                var validActualResult = InvokeMethod(controller, "AddDecoration", addDecorationArguments);

                var validExpectedResult = "Successfully added Ornament.";

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
}
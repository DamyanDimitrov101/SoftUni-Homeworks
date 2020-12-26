using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionLab
{
    [Author("Damyan")]
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass,params string[] arrNames)
        {
            Type classtype = Type.GetType($"ReflectionLab.{investigatedClass}");
            FieldInfo[] fields = classtype.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var sb = new StringBuilder();
            
            object instanceOfHacker = Activator.CreateInstance(classtype, new object[] { });

            sb.AppendLine($"Class under investigation: {investigatedClass}");


            foreach (var field in fields.Where(f=>arrNames.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(instanceOfHacker)}");
            }
            

            return sb.ToString().Trim();
        }

        public string AnalyzeAcessModifiers(string className)
        {
            var sb = new StringBuilder();

            Type type = Type.GetType($"ReflectionLab.{ className}");
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            MethodInfo[] methodInfosPublic = type.GetMethods(BindingFlags.Public|BindingFlags.Instance);
            MethodInfo[] methodInfosPrivate = type.GetMethods(BindingFlags.NonPublic|BindingFlags.Instance);

            foreach (var field in fieldInfos)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }
            foreach (var method in methodInfosPrivate.Where(m=>m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }
            foreach (var method in methodInfosPublic.Where(m=>m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().Trim();
        }

        public string RevealPrivateMethods(string className)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"All Private Methods of Class: {className}");

            Type type = Type.GetType($"ReflectionLab.{className}");
            Type baseType = type.BaseType;

            sb.AppendLine($"Base Class: {baseType.Name}");

            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance);

            foreach (var prMethod in methodInfos)
            {
                sb.AppendLine($"{prMethod.Name}");
            }

            return sb.ToString().Trim();
        }

        [Author("Damyan")]
        public string CollectGettersAndSetters(string className)
        {
            var sb = new StringBuilder();

            Type type = Type.GetType($"ReflectionLab.{className}");

            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var method in methodInfos.Where(m=>m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }

            foreach (var method in methodInfos.Where(m=>m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().Trim();
        }
    }
}

﻿namespace DeprecatedClass
{
    using System.Reflection;

    public class PrivateObject
    {
        private object _obj;

        public PrivateObject(object obj){
            this._obj = obj;
        }


        public object Invoke(string methodName, params object[] parameters){
            var type = this._obj.GetType();
            var method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var Instance  = Activator.CreateInstance(type);
            var result = method.Invoke(Instance,parameters);

            return result;
        }
    }
}

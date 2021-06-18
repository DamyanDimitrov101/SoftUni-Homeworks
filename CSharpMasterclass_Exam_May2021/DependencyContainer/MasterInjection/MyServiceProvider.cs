namespace MasterInjection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MyServiceProvider : IMyServiceProvider
    {
        private IList<Type> interfaceCollection;

        public MyServiceProvider()
        {
            this.interfaceCollection = new List<Type>();
        }

        public void Add<TSource, TDestination>()
            where TDestination : TSource
        {
        }

        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }

        public T CreateInstance<T>()
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace ValidationAttributes.MyAttributes
{
    public abstract class MyValidationAttribute : Attribute
    {
        public abstract bool IsValid(object obj);       
    }
}

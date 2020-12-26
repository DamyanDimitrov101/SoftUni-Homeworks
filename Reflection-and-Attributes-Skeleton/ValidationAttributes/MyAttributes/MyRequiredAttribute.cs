using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationAttributes.MyAttributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true)]
    public class MyRequiredAttribute : MyValidationAttribute
    {
        public override bool IsValid(object obj)
        {
            return obj != null;
        }
    }
}

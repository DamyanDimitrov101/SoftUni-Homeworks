using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationAttributes.MyAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MyRangeAttribute : MyValidationAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;

        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override bool IsValid(object obj)
        {
            if (obj is int objAsInt)
            {
                if (objAsInt>=minValue&&objAsInt<=maxValue)
                {
                    return true;
                }
                return false;
            }
            throw new ArgumentException("Invalid type!");
        }
    }
}

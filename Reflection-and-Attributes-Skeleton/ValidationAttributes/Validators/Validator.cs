using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ValidationAttributes.Models;
using ValidationAttributes.MyAttributes;

namespace ValidationAttributes.Validators
{
    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();

            foreach (var prop in propertyInfos)
            {
                IEnumerable<MyValidationAttribute> attributes = prop
                    .GetCustomAttributes()
                    .Where(a=>a is MyValidationAttribute)
                    .Cast<MyValidationAttribute>();

                foreach (var attrib in attributes)
                {
                    bool result = attrib.IsValid(prop.GetValue(obj));

                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

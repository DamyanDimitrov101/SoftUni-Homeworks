using LoggerLib.Layouts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Interpreters
{
    public class LayoutInterpreter
    {
        public ILayout GetLayout(string layoutAsString)
        {
            Type type = Type.GetType(layoutAsString);
            if (type is ILayout layout)
            {
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    return null;
                }
                return instance as ILayout;
            }
            return null;
        }
    }
}

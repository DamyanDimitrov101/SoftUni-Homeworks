using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string Format { get; } = "{0} - {1} - {2}";
    }
}

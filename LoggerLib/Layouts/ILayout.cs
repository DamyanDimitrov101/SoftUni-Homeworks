using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Layouts
{
    public interface ILayout
    {
        string Format { get; }
    }
}

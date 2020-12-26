using LoggerLib.Appenders;
using LoggerLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Interpreters
{
    public interface IInterpreter
    {
        ReportLevel ReportLevel { get; set; }
    }
}

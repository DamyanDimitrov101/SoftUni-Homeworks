using LoggerLib.Enums;
using LoggerLib.Layouts;
using LoggerLib.Loggers;
using System;

namespace LoggerLib.Appenders
{
    public class ConsoleAppender : IAppender
    {
        public ConsoleAppender(ILayout layout,ILogFile logFile)
        {
            this.LayoutCurrent = layout;
        }

        public ILayout LayoutCurrent { get; }
        public ReportLevel ReportLevel { get; set; } = ReportLevel.info;

        public void Append(string dateTime,ReportLevel logLevel,string message)
        {
            if (logLevel>=this.ReportLevel)
            {
                Console.WriteLine(string.Format(this.LayoutCurrent.Format, dateTime, logLevel, message));
            }
        }
    }
}

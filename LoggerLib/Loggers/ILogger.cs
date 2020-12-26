using LoggerLib.Appenders;
using System.Collections.Generic;

namespace LoggerLib.Loggers
{
    public interface ILogger
    {
        List<IAppender> Appenders{ get; }
        void Error(string dateTime,string message);
        void Info(string dateTime, string message);
        void Fatal(string dateTime, string message);
        void Critical(string dateTime, string message);
    }
}

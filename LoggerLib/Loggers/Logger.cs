using LoggerLib.Appenders;
using LoggerLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggerLib.Loggers
{
    public class Logger : ILogger
    {
        private List<IAppender> appenders;

        public Logger(params IAppender[] appender)
        {
            this.Appenders = appender.ToList();
        }


        public List<IAppender> Appenders 
        { 
            get => this.appenders; 
            private set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appenders),"Value cannot be null.");
                }
                this.appenders = value;
            } 
        }
        public void Error(string dateTime, string message)
        {
            AppendMessage(dateTime, ReportLevel.error, message);
        }

        public void Info(string dateTime, string message)
        {
            AppendMessage(dateTime, ReportLevel.info, message);
        }

        public void Fatal(string dateTime, string message)
        {
            AppendMessage(dateTime, ReportLevel.fatal, message);
        }
        public void Critical(string dateTime, string message)
        {
            AppendMessage(dateTime,ReportLevel.critical,message);
        }

        public void Warning(string dateTime, string message)
        {
            AppendMessage(dateTime, ReportLevel.warning, message);
        }

        private void AppendMessage(string dateTime, ReportLevel logLevel, string message)
        {
            foreach (var appender in Appenders)
            {
                appender.Append(dateTime, logLevel, message);
            }
        }
    }
}

using LoggerLib.Appenders;
using LoggerLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Interpreters
{
    public class CommandInterpreter : IInterpreter
    {
        public CommandInterpreter(params IAppender[] appenders) 
        {
            this.Appenders = appenders;
        }

        public IAppender[] Appenders { get; }

        public ReportLevel ReportLevel { get; set; }

        public void Evaluate(string message)
        {

            string[] arr = message.Split("|", StringSplitOptions.RemoveEmptyEntries);

                string logLevel = arr[0];

            ReportLevelInterpreter reportLevelInterpreter = new ReportLevelInterpreter(logLevel);
            this.ReportLevel = reportLevelInterpreter.ReturnReportLevel();

            foreach (var apppender in this.Appenders)
            {
                apppender.Append(arr[1], ReportLevel, arr[2]);
            }

        }
    }
}

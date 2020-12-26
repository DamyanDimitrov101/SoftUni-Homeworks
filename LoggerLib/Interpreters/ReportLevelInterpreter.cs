using LoggerLib.Appenders;
using LoggerLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Interpreters
{
    public class ReportLevelInterpreter : IInterpreter
    {
        private string logLevelAsString;

        public ReportLevelInterpreter(string logLevelAsString)
        {
            this.logLevelAsString = logLevelAsString;
        }
        public ReportLevel ReportLevel { get; set; }

        public ReportLevel ReturnReportLevel()
        {
            switch (this.logLevelAsString?.ToLower())
            {
                case "info":
                    this.ReportLevel = ReportLevel.info;
                    break;
                case "warning":
                    this.ReportLevel = ReportLevel.warning;
                    break;
                case "error":
                    this.ReportLevel = ReportLevel.error;
                    break;
                case "critical":
                    this.ReportLevel = ReportLevel.critical;
                    break;
                case "fatal":
                    this.ReportLevel = ReportLevel.fatal;
                    break;
            }

            return this.ReportLevel;
        }
    }
}

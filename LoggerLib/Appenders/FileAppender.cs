using LoggerLib.Enums;
using LoggerLib.Layouts;
using LoggerLib.Loggers;

namespace LoggerLib.Appenders
{
    public class FileAppender : IAppender
    {
        public FileAppender(ILayout layout, ILogFile logFile)
        {
            this.LayoutCurrent = layout;
            this.logFile = logFile;
        }

        public ILayout LayoutCurrent { get; }
        public ILogFile logFile { get; }
        public ReportLevel ReportLevel { get; set; } = ReportLevel.info;

        public void Append(string dateTime, ReportLevel logLevel, string message)
        {
            if (logLevel >= this.ReportLevel)
            {
                logFile.Write(string.Format(LayoutCurrent.Format, dateTime, logLevel, message) + System.Environment.NewLine);
            }
        }
    }
}

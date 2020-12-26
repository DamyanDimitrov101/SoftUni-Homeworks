using LoggerLib.Enums;
using LoggerLib.Layouts;

namespace LoggerLib.Appenders
{
    public interface IAppender
    {
        ReportLevel ReportLevel { get; set; }
        ILayout LayoutCurrent { get; }
        void Append(string dateTime, ReportLevel logLevel, string message);
    }
}

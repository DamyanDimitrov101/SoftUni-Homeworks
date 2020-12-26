using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLib.Loggers
{
    public interface ILogFile
    {
        string path { get; }

        void Write(string message);

        int Size { get; }
    }
}

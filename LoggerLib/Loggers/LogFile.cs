using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoggerLib.Loggers
{
    public class LogFile : ILogFile
    {
        public string path => @"../../../output.txt";

        public int Size => File.ReadAllText(path).Where(char.IsLetter).Sum(x=>x);

        public void Write(string message)
        {
            File.AppendAllText(path, message);
        }
    }
}

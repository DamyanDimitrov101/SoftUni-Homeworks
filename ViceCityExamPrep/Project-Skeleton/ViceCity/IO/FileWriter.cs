using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ViceCity.IO.Contracts;

namespace ViceCity.IO
{
    public class FileWriter : IWriter
    {
        private string path = @"../../../output.txt";
        private FileStream fs;
        private StreamWriter sw;

        public FileWriter()
        {
            this.fs = new FileStream(path, FileMode.OpenOrCreate);
            this.sw = new StreamWriter(fs);

            sw.AutoFlush = true;

            Console.SetOut(sw);
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}

using MXGP.IO.Contracts;
using System;
using System.IO;

namespace MXGP.IO
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

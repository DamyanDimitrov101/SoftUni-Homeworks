using DWAsync.Core;
using DWAsync.Models;
using DWAsync.Models.Contracts;

namespace DWAsync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IReader fileReader = new FileReader(); 
            IWriter fileWriter = new FileWriter();

            var engine = new Engine(fileWriter, fileReader);

            engine.Run();
        }
    }
}

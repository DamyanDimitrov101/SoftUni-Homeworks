namespace DWAsync.Models
{
    using System.IO;
    using System.Threading.Tasks;
    using Contracts;

    public class FileWriter : IWriter
    {
        public async Task SaveFile(string filepath, string data)
        {
            System.Console.WriteLine(data);
            using (StreamWriter stream = new StreamWriter(filepath))
            {
                int length = data.Length;

                await stream.WriteAsync(data);
            }
        }

    }
}

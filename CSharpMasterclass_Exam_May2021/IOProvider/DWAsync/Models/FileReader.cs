namespace DWAsync.Models
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class FileReader : IReader
    {
        public async Task<string> ReadFileAsync(string filePath)
        {
            //using var sourceStream =
            //    new FileStream(
            //filePath,
            //FileMode.Open, FileAccess.Read, FileShare.Read,
            //bufferSize: 4096, useAsync: true);

            Console.WriteLine("Reading...");

            byte[] result;

            int read;

            using (FileStream reader = File.Open(filePath, FileMode.Open))
            {
                using var ms = new MemoryStream();
                result =  new byte[reader.Length];

                while ((read = await reader.ReadAsync(result, 0, result.Length)) > 0)
                {
                    ms.Write(result, 0, read);
                    Console.WriteLine(ms.ToArray());

                }

                //var res = await reader.ReadAsync(result, 0, (int)reader.Length);

                Console.WriteLine("File readed");
            }

            string text = System.Text.Encoding.UTF8.GetString(result);
            Console.WriteLine(text);



            return text;
        }
    }
}

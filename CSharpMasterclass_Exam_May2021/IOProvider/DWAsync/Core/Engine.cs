namespace DWAsync.Core
{
    using Contracts;
    using Models.Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly IWriter fileWriter;
        private readonly IReader consoleReader;

        public Engine(IWriter fileWriter, IReader consoleReader)
        {
            this.fileWriter = fileWriter;
            this.consoleReader = consoleReader;
        }

        public async void Run()
        {
            for (int i = 1; i <= 8; i++)
            {
                var fileName = i + ".txt";

                var filePath = $"../../../Files/{fileName}";
                var content = await consoleReader.ReadFileAsync(filePath);

                var newTextFilePath = $"../../../Files/{i}New.txt";

                await this.fileWriter.SaveFile(newTextFilePath, content);

                Console.WriteLine("Success!");
            }

        }
    }
}

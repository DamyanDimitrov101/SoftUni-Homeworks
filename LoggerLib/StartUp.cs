using LoggerLibrary.Core;
using System;

namespace LoggerLibrary
{
    public static class StartUp
    {
        public static void Main()
        {
            Engine engine = new Engine();
            engine.Run();
        }
    }
}

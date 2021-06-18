using HttpServer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public static class StartUp
    {
        public static async Task Main()
        {
            var address = "127.0.0.1";
            var port = 9090;

            var server = new Server(address, port);

            await server.Start();
        }
    }
}

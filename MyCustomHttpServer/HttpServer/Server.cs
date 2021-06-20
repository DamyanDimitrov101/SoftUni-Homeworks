using HttpServer.Http;
using MyCustomHttpServer.HttpServer.Http;
using MyCustomHttpServer.HttpServer.Routing;
using MyCustomHttpServer.HttpServer.Routing.Contracts;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Server
    {
        private readonly IPAddress iPAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly IRoutingTable routingTable;

        public Server(string ipAddress, int port, Action<IRoutingTable> routingTableConfig)
        {
            this.iPAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.iPAddress, port);

            this.routingTable = new RoutingTable();
            routingTableConfig(this.routingTable);
        }

        public Server(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1",port, routingTable)
        {
        }

        public Server(Action<IRoutingTable> routingTable)
            : this(5050, routingTable)
        {

        }

        public async Task Start()
        {

            listener.Start();

            Console.WriteLine($"Server started on port {port}.");
            Console.WriteLine("Listening for requests...");


            while (true)
            {
                var connection = await listener.AcceptTcpClientAsync();

                NetworkStream networkStream = connection.GetStream();

                var requestText =  await this.ReadRequest(networkStream);

                if (requestText==string.Empty)
                {
                    connection.Close();
                    continue;
                }
                Console.WriteLine(requestText);

                var httpRequest = HttpRequest.Parse(requestText);

                var response = this.routingTable.MatchRequest(httpRequest);

                await WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream stream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();
            var totalBytes = 0;

            do
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (totalBytes > 10* 1024)
                {
                    throw new InvalidOperationException("Request is too large!");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            }
            while (stream.DataAvailable);


                return requestBuilder.ToString();
        }


        private async Task WriteResponse(
                NetworkStream stream,
                HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await stream.WriteAsync(responseBytes);
        }
    }
}

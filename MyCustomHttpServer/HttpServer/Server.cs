using HttpServer.Http;
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

        public Server(string ipAddress, int port)
        {
            this.iPAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.iPAddress, port);
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

                await WriteResponse(networkStream);

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


        private async Task WriteResponse(NetworkStream stream)
        {
            var content = @"
<html>    
    <head>
    </head>
    <body>
        Hello from my server!
    </body>
</html>";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"
HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}
";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await stream.WriteAsync(responseBytes);
        }
    }
}

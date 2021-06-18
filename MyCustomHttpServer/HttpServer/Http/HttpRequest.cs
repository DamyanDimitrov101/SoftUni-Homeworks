using MyCustomHttpServer.HttpServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpServer.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split("/r/n");

            var startLine = lines.First().Split(" ");
            var method = ParseHttpMethod(startLine[0]);

            var url = startLine[1];

            var headerLines = lines.Skip(1);

            var headerCollection = ParseHttpHeaderCollection(headerLines);

            var body = string.Join(Environment.NewLine, lines.Skip(headerCollection.Count + 2).ToArray());

            return new HttpRequest
            {
                Method = method,
                Url = url,
                Headers = headerCollection,
                Body = body
            };
        }

        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var line in headerLines)
            {
                if (line == string.Empty)
                {
                    break;
                }

                var headerParts = line.Split(":", 2);

                var header = new HttpHeader
                {
                    Name = headerParts[0],
                    Value = headerParts[1].Trim()
                };

                headerCollection.Add(header);
            }

            return headerCollection;
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method {method} is not supported!")
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode code)
        {
            this.StatusCode = code;

            this.Headers.Add("Server", "My Custom Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }
        public HttpStatusCode StatusCode { get; init; }

        public HttpHeaderCollection Headers { get; } = new();

        public string Content { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();
                result.Append(this.Content);
            }

            return result.ToString();
        }
    }
}

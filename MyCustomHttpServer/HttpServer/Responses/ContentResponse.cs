using MyCustomHttpServer.HttpServer.Common;
using MyCustomHttpServer.HttpServer.Http;
using System;
using System.Text;

namespace MyCustomHttpServer.HttpServer.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.Ok)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }
    }
}

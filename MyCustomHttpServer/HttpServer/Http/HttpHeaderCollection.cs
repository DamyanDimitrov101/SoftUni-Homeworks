using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Http
{
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> _headers;

        public HttpHeaderCollection()
        {
            this._headers = new();
        }

        public int Count => this._headers.Count;

        public void Add(HttpHeader header)
            => this._headers.Add(header.Name, header);
    }
}

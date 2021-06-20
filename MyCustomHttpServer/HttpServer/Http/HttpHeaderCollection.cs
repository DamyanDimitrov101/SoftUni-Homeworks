using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Http
{
    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> _headers;

        public HttpHeaderCollection()
        {
            this._headers = new();
        }

        public int Count => this._headers.Count;

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);

            this._headers.Add(name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
        {
            return this._headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}

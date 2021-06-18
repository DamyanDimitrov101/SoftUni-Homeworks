using System.Collections.Generic;
using System.Linq;
using SIS.HTTP.Common;
using SIS.HTTP.Headers.Contracts;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private Dictionary<string, HttpHeader> httpHeaders;

        public HttpHeaderCollection()
        {
            this.httpHeaders = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.httpHeaders.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.httpHeaders.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            try
            {
                var res = this.httpHeaders[key];
                return res;

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return null;
        }

        public override string ToString() => string.Join("\r\n",
            this.httpHeaders.Values.Select(header => header.ToString()));

    }
}
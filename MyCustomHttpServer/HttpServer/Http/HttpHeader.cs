using MyCustomHttpServer.HttpServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Http
{
    public class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            Name = name;
            Value = value;
        }

        public string Name { get; init; }

        public string Value { get; init; }

        public override string ToString()
         => $"{this.Name}: {this.Value}";
    }
}

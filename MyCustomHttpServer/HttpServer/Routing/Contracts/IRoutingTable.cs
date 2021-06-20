using HttpServer.Http;
using MyCustomHttpServer.HttpServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Routing.Contracts
{
    public interface IRoutingTable
    {
        IRoutingTable Map(string url,HttpMethod method, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse response);

        HttpResponse MatchRequest(HttpRequest request);
    }
}

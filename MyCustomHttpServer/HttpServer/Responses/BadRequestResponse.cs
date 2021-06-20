using MyCustomHttpServer.HttpServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomHttpServer.HttpServer.Responses
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}

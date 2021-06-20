using HttpServer;
using MyCustomHttpServer.Controllers;
using MyCustomHttpServer.HttpServer.Responses;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public static class StartUp
    {
        public static async Task Main() 
            =>await new Server(routes=> routes
                    .MapGet("/", new HtmlResponse("Hello from my server."))
                    .MapGet("/Cats", new HtmlResponse("<h1>Hello from the Cats List.</h1>"))
                    .MapGet("/Dogs", new HtmlResponse("<h1>Hello from the Dogs List.</h1>"))
            ).Start();
        
    }
}

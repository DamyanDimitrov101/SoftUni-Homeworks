using MyCustomHttpServer.HttpServer.Http;


namespace MyCustomHttpServer.HttpServer.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) 
            : base(text, "text/html; charset=UTF-8")
        {
        }
    }
}

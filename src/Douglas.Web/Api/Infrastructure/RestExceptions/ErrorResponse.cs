using Nancy;

namespace Douglas.Web.Api.Infrastructure.RestExceptions
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message, HttpStatusCode statusCode, string contentType)
        {
            
            this.WithStatusCode(statusCode);
            this.WithContentType(contentType);
            this.WithBody(message);
        }        
    }
}
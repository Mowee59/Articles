using System.Net;

namespace Blocks.Exceptions;

public class HttpException : Exception
{

    public HttpException(HttpStatusCode statuscode, string message)
        :base(string.IsNullOrEmpty(message) ? statuscode.ToString() : message)
    {
        this.HttpStatusCode = statuscode;
    }

    public HttpException(HttpStatusCode statuscode, string message, Exception ex)
        : base(message, ex)
    {
        this.HttpStatusCode = statuscode;
    }

    public HttpStatusCode HttpStatusCode { get;  }

}

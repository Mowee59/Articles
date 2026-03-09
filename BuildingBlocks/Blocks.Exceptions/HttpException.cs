using System.Net;

namespace Blocks.Exceptions;

/// <summary>
/// Exception type that carries an associated HTTP status code for API responses.
/// </summary>
public class HttpException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class with the specified
    /// HTTP status code and message. If the message is null or empty, the status code name is used.
    /// </summary>
    /// <param name="statuscode">HTTP status code to associate with the exception.</param>
    /// <param name="message">Error message to use for the exception.</param>
    public HttpException(HttpStatusCode statuscode, string message)
        :base(string.IsNullOrEmpty(message) ? statuscode.ToString() : message)
    {
        this.HttpStatusCode = statuscode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class with the specified
    /// HTTP status code, message, and inner exception.
    /// </summary>
    /// <param name="statuscode">HTTP status code to associate with the exception.</param>
    /// <param name="message">Error message to use for the exception.</param>
    /// <param name="ex">The inner exception that caused this error.</param>
    public HttpException(HttpStatusCode statuscode, string message, Exception ex)
        : base(message, ex)
    {
        this.HttpStatusCode = statuscode;
    }

    /// <summary>
    /// HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get;  }

}

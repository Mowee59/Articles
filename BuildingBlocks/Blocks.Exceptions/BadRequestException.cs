using System.Net;

namespace Blocks.Exceptions;

/// <summary>
/// Exception representing an HTTP 400 Bad Request error.
/// Thrown when a client sends a malformed or invalid request that cannot be processed.
/// </summary>
public class BadRequestException : HttpException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class
    /// with the specified error message.
    /// </summary>
    /// <param name="message">A human-readable description of the validation or request error.</param>
    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class
    /// with the specified error message and inner exception.
    /// </summary>
    /// <param name="message">A human-readable description of the validation or request error.</param>
    /// <param name="exception">The underlying exception that caused this error.</param>
    public BadRequestException(string message, Exception exception) : base(HttpStatusCode.BadRequest, message, exception) { }
   
}

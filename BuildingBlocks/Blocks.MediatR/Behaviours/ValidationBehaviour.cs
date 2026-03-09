

using FluentValidation;
using MediatR;

namespace Blocks.MediatR.Behaviours;

/// <summary>
/// MediatR pipeline behavior that runs all registered FluentValidation validators
/// for a request before it reaches the handler and throws on validation failures.
/// </summary>
/// <typeparam name="TRequest">Request type being validated.</typeparam>
/// <typeparam name="TResponse">Handler response type.</typeparam>
public class ValidationBehaviour<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Executes validators for the request and short-circuits the pipeline by throwing
    /// a <see cref="ValidationException"/> if any validation errors are found.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">Delegate to invoke the next step in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the request handler when validation succeeds.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = 
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = 
            validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if(failures.Any())
            throw new ValidationException(failures);

        return await next();
    }

}

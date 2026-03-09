using Blocks.Domain;
using MediatR;

namespace Blocks.MediatR.Behaviours;

/// <summary>
/// MediatR pipeline behavior that sets the <see cref="IAuditableAction.CreatedById"/>
/// for auditable requests before they reach the handler.
/// </summary>
/// <typeparam name="TRequest">Request type implementing <see cref="IAuditableAction"/>.</typeparam>
/// <typeparam name="TResponse">Handler response type.</typeparam>
public class SetUserIdBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuditableAction
{
    /// <summary>
    /// Populates the <see cref="IAuditableAction.CreatedById"/> on the request, then
    /// invokes the next behavior/handler in the MediatR pipeline.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">Delegate to invoke the next step in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the request handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        request.CreatedById = 1; //Todo: Get the user id from the context and set it here

        return await next();
    }
}

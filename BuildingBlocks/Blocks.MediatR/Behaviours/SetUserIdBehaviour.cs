using Blocks.Domain;
using MediatR;

namespace Blocks.MediatR.Behaviours;

public class SetUserIdBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuditableAction
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        request.CreatedById = 1; //Todo: Get the user id from the context and set it here

        return await next();
    }
}

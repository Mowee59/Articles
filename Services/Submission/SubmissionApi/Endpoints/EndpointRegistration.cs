using Submission.Api.Endpoints;

namespace Submission.API.Endpoints;

/// <summary>
/// Aggregates all minimal API endpoint mappings for the Submission service.
/// </summary>
public static  class EndpointRegistration
{
    /// <summary>
    /// Registers all endpoints for the Submission API on the provided route builder.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure routes.</param>
    /// <returns>The same endpoint route builder for chaining.</returns>
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        CreateArticleEndpoint.Map(app);
        AssignAuthorEndpoint.Map(app);
        CreateAndAssignAuthorEndpoint.Map(app);
        UploadManuscriptFileEndpoint.Map(app);
        // TODO - Add other endpoints
        return app;
    }

}


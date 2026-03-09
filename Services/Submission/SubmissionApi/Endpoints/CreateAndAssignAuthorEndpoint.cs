using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Submission.Application.Features.CreateAndAssignAuthor;

namespace Submission.API.Endpoints;

/// <summary>
/// Minimal API endpoint mapping for creating an author and assigning them to an article.
/// </summary>
public static class CreateAndAssignAuthorEndpoint
{

    /// <summary>
    /// Maps the POST endpoint used to create a new author and attach them to an existing article.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure routes.</param>
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles/{articleId:int}/authors", async (int articleId, CreateAndAssignAuthorCommand command, ISender sender) =>
        {
            var response = await sender.Send(command);
            return Results.Ok(response);
        })
            .RequireRoleAuthorization(Role.AUT)
            .WithName("CreateAndAssignAuthor")
            .WithTags("Articles")
            .Produces<IdResponse>(StatusCodes.Status200OK)
            .ProducesProblem((StatusCodes.Status400BadRequest))
            .ProducesProblem((StatusCodes.Status404NotFound))
            .ProducesProblem((StatusCodes.Status401Unauthorized));
    }

}

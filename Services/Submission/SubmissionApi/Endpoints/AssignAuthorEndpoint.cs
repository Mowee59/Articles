using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Submission.Application.Features.AssignAuthor;


namespace Submission.API.Endpoints;

/// <summary>
/// Minimal API endpoint mapping for assigning an author to an article.
/// </summary>
public static class AssignAuthorEndpoint
{
    /// <summary>
    /// Maps the POST endpoint used to assign an author to an article.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure routes.</param>
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles/{articleId:int}/assign-author/{authorId:int}", async (int articleId, int authorId, AssignAuthorCommand command, ISender
            sender) =>
            {

                var response = await sender.Send(command with { ArticleId = articleId, AuthorId = authorId });
                return Results.Ok(response);

            })
            .RequireRoleAuthorization(Role.CORAUT)
            .WithName("AssignAuthor")
            .WithTags("Articles")
            .Produces<IdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized);

    }
}


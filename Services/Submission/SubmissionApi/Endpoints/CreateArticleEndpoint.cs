using Articles.Abstractions.Enums;
using MediatR;
using Submission.Application.Features.CreateArticle;

namespace Submission.Api.Endpoints;

/// <summary>
/// Minimal API endpoint mapping for creating a new article.
/// </summary>
public static class CreateArticleEndpoint
{
    /// <summary>
    /// Maps the POST endpoint used to create an article.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure routes.</param>
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles", async ( CreateArticleCommand command, ISender sender) => 
        {
            var response = await sender.Send(command);
            return Results.Created($"api/articles/{response.Id}", response);
        })
            .RequireAuthorization(policy => policy.RequireRole(Role.AUT))
            .WithName("CreateArticle")
            .WithTags("Articles")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem((StatusCodes.Status401Unauthorized))
            .ProducesValidationProblem((StatusCodes.Status400BadRequest));

    }
}


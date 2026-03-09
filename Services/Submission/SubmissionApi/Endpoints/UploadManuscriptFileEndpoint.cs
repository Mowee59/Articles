using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.UploadFile;

namespace Submission.API.Endpoints;

/// <summary>
/// Minimal API endpoint mapping for uploading a manuscript file for an article.
/// </summary>
public static class UploadManuscriptFileEndpoint
{
    /// <summary>
    /// Maps the POST endpoint used to upload a manuscript file and returns a created asset reference.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure routes.</param>
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.MapPost("/articles/{articleId:int}/assets/manuscript:upload",
            async ([FromRoute] int articleId, [FromForm] UploadManuscriptFileCommand command, ISender sender) =>
            {
                var response = await sender.Send(command with { ArticleId = articleId });
                return Results.Created($"/api/articles/{articleId}/assets/{response.Id}:download", response);
            })
        .RequireRoleAuthorization(Role.AUT)
        .WithName("UploadManuscript")
        .WithTags("Assets")
        .Produces<IdResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .DisableAntiforgery(); // because of IFormFile
    }
}
namespace Submission.Api.Endpoints;

    public static class CreateArticleEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/articles", async () => { })
                .RequireAuthorization(policy => policy.RequireRole("AUT"))
                .WithName("CreateArticle")
                .WithTags("Articles")
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem((StatusCodes.Status401Unauthorized))
                .ProducesValidationProblem((StatusCodes.Status400BadRequest));

        }
    }


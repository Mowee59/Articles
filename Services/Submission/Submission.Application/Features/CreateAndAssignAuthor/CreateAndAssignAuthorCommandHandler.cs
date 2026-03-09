using Blocks.EntityFramework;

namespace Submission.Application.Features.CreateAndAssignAuthor;

/// <summary>
/// Handles <see cref="CreateAndAssignAuthorCommand"/> requests by resolving or creating
/// an author and assigning them to the target article.
/// </summary>
public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository)
    : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
    /// <summary>
    /// Loads the article, either creates a new author when no user id is provided
    /// or uses an existing user (to be implemented), assigns the author with their
    /// contribution areas, saves changes, and returns the article identifier.
    /// </summary>
    /// <param name="command">The create-and-assign-author command.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="IdResponse"/> containing the article id (or author id in future if needed).</returns>
    public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        Author? author = null;
        if (command.UserId == null) // Author is not an user
            author = Author.Create(command.Email, command.FirstName!, command.LastName!, command.Title, command.Affiliation!);
        else { } //todo Author is an User

        article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

        await _articleRepository.SaveChangesAsync(ct);

        return new IdResponse(article.Id); // Todo - return author id instead of article id if needed
    }
}
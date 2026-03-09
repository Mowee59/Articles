using Blocks.EntityFramework;

namespace Submission.Application.Features.AssignAuthor;

/// <summary>
/// Handles <see cref="AssignAuthorCommand"/> requests by attaching an author to an article
/// and persisting the change.
/// </summary>
public class AssignAuthorCommandHandler(ArticleRepository _articleRepository)
    : IRequestHandler<AssignAuthorCommand, IdResponse>
{
    /// <summary>
    /// Loads the target article and author, assigns the author with their contribution areas,
    /// saves the changes, and returns the article identifier.
    /// </summary>
    /// <param name="command">The assign-author command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An <see cref="IdResponse"/> containing the updated article id.</returns>
    public async Task<IdResponse> Handle(AssignAuthorCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var author = await _articleRepository.Context.Authors.FindByIdOrThrowAsync(command.AuthorId);

        article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(article.Id);

    }
}
